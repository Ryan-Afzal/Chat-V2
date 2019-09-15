using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Chat_V2.Models.Command;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat_V2.Hubs {
	public class ChatHub : Hub {

		public UserManager<ChatUser> UserManager { get; }
		public ChatContext ChatContext { get; }

		public ChatHub(UserManager<ChatUser> userManager, ChatContext context) {
			UserManager = userManager;
			ChatContext = context;
		}
		
		public async Task GetPreviousMessages(int groupId, int rankOrd, int prevStart, int prevEnd) {
			var group = GetGroupById(groupId);//Get the actual group object

			//basic test code that just gets every single previous message regardless of prevStart and prevEnd.
			LinkedList<IncomingMessageArgs> list = new LinkedList<IncomingMessageArgs>();
			int i = group.ChatMessages.Count - 1;
			foreach (var m in group.ChatMessages) {
				var user = await UserManager.FindByIdAsync(m.ChatUserID + "");
				var rank = PermissionRank.GetPermissionRankByOrdinal(rankOrd);
				var messageRank = PermissionRank.GetPermissionRankByOrdinal(m.ChatUserRank);//rank of user who sent the message
				
				if (rank.CompareTo(PermissionRank.GetPermissionRankByOrdinal(m.MinRank)) >= 0) {
					if (i < prevEnd) {
						if (i < prevStart) {
							break;
						}
						
						list.AddFirst(new IncomingMessageArgs() {
							SenderID = user.Id,
							SenderName = user.UserName,
							SenderRankOrdinal = messageRank.Ordinal,
							SenderRankName = messageRank.Name,
							SenderRankColor = messageRank.Color,
							Message = m.Message
						});
					}
					
					i--;
				}
			}

			await Clients.Caller.SendAsync("ReceivePreviousMessages", list);
		}

		public async Task ProcessCommand(OutgoingCommandArgs args) {
			//Load data from database based on args
			PermissionRank senderRank = PermissionRank.GetPermissionRankByOrdinal(args.SenderRank);
			ChatUser sender = await UserManager.FindByIdAsync(args.SenderID + "");
			Group group = GetGroupById(args.GroupID);
			string text = args.Text;

			await Clients.User(Context.UserIdentifier)
				.SendAsync(
					"ReceiveCommandMessage", 
					new IncomingCommandMessageArgs() {
						Color = "000000",
						Message = text
					}
					);

			string[] splitText = text.Split(' ');
			if (ChatContext.CommandList.DoesCommandExist(splitText[0])) {
				ICommand command = ChatContext.CommandList.GetCommandByName(splitText[0]);
				if (command.MinRank <= senderRank.Ordinal) {
					string[] textArgs = new string[splitText.Length - 1];
					for (int i = 1; i < splitText.Length; i++) {
						textArgs[i - 1] = splitText[i]; 
					}

					await command.Execute(
						new CommandArgs() {
							Args = textArgs,
							Hub = this,
							Group = group,
							User = sender,
							UserRank = senderRank
						}
						);

					return;
				}
			}

			await Clients.User(Context.UserIdentifier)
				.SendAsync(
					"ReceiveCommandMessage",
					new IncomingCommandMessageArgs() {
						Color = "FF0000",
						Message = $"ERROR: {splitText[0]} is not a valid or accessible command."
					}
					);
		}

		public async Task SendMessage(OutgoingMessageArgs args) {
			//Load data from database based on args
			PermissionRank senderRank = PermissionRank.GetPermissionRankByOrdinal(args.SenderRank);
			PermissionRank minRank = PermissionRank.GetPermissionRankByOrdinal(args.MinRank);
			ChatUser sender = await UserManager.FindByIdAsync(args.SenderID + "");
			Group group = GetGroupById(args.GroupID);

			//Log the message
			ChatMessage chatMessage = new ChatMessage() {
				ChatUserID = sender.Id,
				GroupID = group.GroupID,
				TimeStamp = DateTime.Now,
				Message = args.Message,
				ChatUserRank = senderRank.Ordinal,
				MinRank = minRank.Ordinal
			};
			group.ChatMessages.Add(chatMessage);
			ChatContext.SaveChanges();

			//Distribute the message
			await GetClientsInGroup(group, minRank)
				.SendAsync(
					"ReceiveMessage", 
					new IncomingMessageArgs() {
						SenderID = args.SenderID,
						SenderName = sender.UserName,
						SenderRankOrdinal = senderRank.Ordinal,
						SenderRankName = senderRank.Name,
						SenderRankColor = senderRank.Color,
						Message = args.Message
					});
		}

		public async Task ClientConnected(ClientConnectedArgs args) {
			var chatUser = ChatContext.Users
					.Include(u => u.Memberships)
					.FirstOrDefault(u => u.Id == args.SenderID);

			var membershipQuery = from membership in chatUser.Memberships
								  where membership.GroupID == args.GroupID
								  select membership;

			//Get if the query returned anything
			var m = membershipQuery.FirstOrDefault();
			if (m == null) {
				//Should never happen, but just in case...
				throw new ArgumentException($"User with ID {args.SenderID} is not a member of group with ID {args.GroupID}");
			}

			m.IsActive = true;
			await ChatContext.SaveChangesAsync();

			await Clients.Caller.SendAsync(
				"ReceiveConnectedUsers",
				from membership in GetGroupById(args.GroupID).Memberships
				where membership.IsActive
				select new UserConnectedArgs() {
					UserID = membership.ChatUserID,
					UserName = ChatContext.Users.FirstOrDefault(u => u.Id == membership.ChatUserID).UserName,
					UserRankName = PermissionRank.GetPermissionRankByOrdinal(membership.Rank).Name
				}
				);

			await GetClientsInGroup(args.GroupID, PermissionRank.USER)
				.SendAsync(
				"UserConnected",
				new UserConnectedArgs() {
					UserID = chatUser.Id,
					UserName = chatUser.UserName,
					UserRankName = PermissionRank.GetPermissionRankByOrdinal(m.Rank).Name
				});
		}

		public async Task ClientDisconnected(ClientDisconnectedArgs args) {
			var chatUser = ChatContext.Users
					.Include(u => u.Memberships)
					.FirstOrDefault(u => u.Id == args.SenderID);
			var query = from membership in chatUser.Memberships
						where membership.GroupID == args.GroupID
						select membership;

			//Get if the query returned anything
			var m = query.FirstOrDefault();
			if (m == null) {
				//Should never happen, but just in case...
				throw new ArgumentException($"User with ID {args.SenderID} is not a member of group with ID {args.GroupID}");
			}

			m.IsActive = false;
			await ChatContext.SaveChangesAsync();

			await GetClientsInGroup(args.GroupID, PermissionRank.USER)
				.SendAsync(
				"UserDisconnected", 
				new UserDisconnectedArgs() {
					UserID = chatUser.Id
				});
		}

		private Group GetGroupById(int groupId) {
			var group = ChatContext.Group
				.Include(g => g.Memberships)
				.Include(g => g.ChatMessages)
				.FirstOrDefault(g => g.GroupID == groupId);

			if (group == null) {
				throw new ArgumentException("Invalid Group ID");
			}

			return group;
		}

		private IReadOnlyList<string> GetUsersInGroup(int groupId, PermissionRank minRank) {
			return GetUsersInGroup(GetGroupById(groupId), minRank);
		}

		private IReadOnlyList<string> GetUsersInGroup(Group group, PermissionRank minRank) {
			List<string> list = new List<string>();

			foreach (var m in group.Memberships) {
				if (m.Rank >= minRank.Ordinal) {
					list.Add(m.ChatUserID + "");
				}
			}

			return list.AsReadOnly();
		}

		private IClientProxy GetClientsInGroup(Group group, PermissionRank minRank) {
			return Clients.Users(GetUsersInGroup(group, minRank));
		}

		private IClientProxy GetClientsInGroup(int groupId, PermissionRank minRank) {
			return Clients.Users(GetUsersInGroup(groupId, minRank));
		}

	}
}

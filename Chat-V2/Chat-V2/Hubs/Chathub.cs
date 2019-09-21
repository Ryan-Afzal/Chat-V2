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

		public async Task Connected(ConnectedArgs args) {
			var chatUser = await ChatContext.Users
				.Include(u => u.Memberships)
				.ThenInclude(m => m.Group)
				.FirstOrDefaultAsync(u => u.Id == args.UserID);
			chatUser.IsOnline = true;
			await ChatContext.SaveChangesAsync();

			foreach (var m in chatUser.Memberships) {
				await Clients.Caller.SendAsync(
					"AddGroup", 
					new AddGroupArgs() {
						GroupID = m.GroupID,
						MembershipID = m.MembershipID,
						GroupName = m.Group.Name
					});
			}
		}

		public async Task Disconnected(DisconnectedArgs args) {
			var chatUser = await UserManager.FindByIdAsync(args.UserID + "");
			chatUser.IsOnline = false;
			await ChatContext.SaveChangesAsync();
		}

		public async Task ConnectedToGroup(ConnectedToGroupArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
				.ThenInclude(g => g.Memberships)
				.ThenInclude(m => m.ChatUser)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			membership.IsOnlineInGroup = true;
			await ChatContext.SaveChangesAsync();

			await GetPreviousMessages(new GetPreviousMessagesArgs() {
				MembershipID = args.MembershipID,
				StartIndex = 0,
				EndIndex = 50
			});

			foreach (var _m in membership.Group.Memberships) {
				if (_m.IsOnlineInGroup) {
					await Clients.Caller.SendAsync(
						"OtherUserConnectedToGroup", 
						new OtherUserConnectedToGroupArgs() {
							GroupID = _m.GroupID,
							UserID = _m.ChatUserID,
							UserName = _m.ChatUser.UserName,
							UserRank = PermissionRank.GetPermissionRankByOrdinal(_m.Rank).Name
						});
					if (_m.IsActiveInGroup) {
						await Clients.Caller.SendAsync(
							"OtherUserActiveInGroup",
							new OtherUserConnectedToGroupArgs() {
								GroupID = _m.GroupID,
								UserID = _m.ChatUserID
							});
					} else {
						await Clients.Caller.SendAsync(
							"OtherUserInactiveInGroup",
							new OtherUserConnectedToGroupArgs() {
								GroupID = _m.GroupID,
								UserID = _m.ChatUserID
							});
					}
				}
			}

			var proxy = GetClientsInGroup(membership.GroupID, PermissionRank.USER);

			await proxy
				.SendAsync(
				"OtherUserConnectedToGroup",
				new OtherUserConnectedToGroupArgs() {
					GroupID = membership.GroupID,
					UserID = membership.ChatUserID,
					UserName = membership.ChatUser.UserName,
					UserRank = PermissionRank.GetPermissionRankByOrdinal(membership.Rank).Name
				});

			await proxy
				.SendAsync(
				"OtherUserActiveInGroup", 
				new OtherUserActiveInGroupArgs() {
					GroupID = membership.GroupID,
					UserID = membership.ChatUserID
				});
		}

		public async Task DisconnectedFromGroup(DisconnectedFromGroupArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			membership.IsOnlineInGroup = false;
			await ChatContext.SaveChangesAsync();

			await GetClientsInGroup(membership.GroupID, PermissionRank.USER)
				.SendAsync(
				"OtherUserDisconnectedFromGroup",
				new OtherUserDisconnectedFromGroupArgs() {
					UserID = membership.ChatUserID, 
					GroupID = membership.GroupID
				});
		}

		public async Task ActiveInGroup(ActiveInGroupArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
				.ThenInclude(g => g.Memberships)
				.ThenInclude(m => m.ChatUser)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			membership.IsActiveInGroup = true;
			await ChatContext.SaveChangesAsync();

			await GetClientsInGroup(membership.GroupID, PermissionRank.USER)
				.SendAsync(
				"OtherUserActiveInGroup",
				new OtherUserActiveInGroupArgs() {
					GroupID = membership.GroupID,
					UserID = membership.ChatUserID
				});
		}

		public async Task InactiveInGroup(InactiveInGroupArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			membership.IsActiveInGroup = false;
			await ChatContext.SaveChangesAsync();

			await GetClientsInGroup(membership.GroupID, PermissionRank.USER)
				.SendAsync(
				"OtherUserInactiveInGroup",
				new OtherUserInactiveInGroupArgs() {
					UserID = membership.ChatUserID,
					GroupID = membership.GroupID
				});
		}

		public async Task GetPreviousMessages(GetPreviousMessagesArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
				.ThenInclude(g => g.ChatMessages)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			//basic test code that just gets every single previous message regardless of prevStart and prevEnd.
			LinkedList<ReceiveMessageArgs> list = new LinkedList<ReceiveMessageArgs>();
			int i = membership.Group.ChatMessages.Count - 1;
			foreach (var m in membership.Group.ChatMessages) {
				var user = await UserManager.FindByIdAsync(m.ChatUserID + "");
				var rank = PermissionRank.GetPermissionRankByOrdinal(membership.Rank);
				var messageRank = PermissionRank.GetPermissionRankByOrdinal(m.ChatUserRank);//rank of user who sent the message

				if (rank.CompareTo(PermissionRank.GetPermissionRankByOrdinal(m.MinRank)) >= 0) {
					if (i < args.EndIndex) {
						if (i < args.StartIndex) {
							break;
						}

						list.AddFirst(new ReceiveMessageArgs() {
							SenderID = user.Id,
							SenderName = user.UserName,
							SenderRankColor = messageRank.Color,
							Message = m.Message,
							Timestamp = m.TimeStamp.ToShortTimeString()
						});
					}

					i--;
				}
			}

			await Clients.Caller.SendAsync(
				"ReceivePreviousMessages",
				list);
		}

		public async Task ProcessCommand(ProcessCommandArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
				.ThenInclude(g => g.ChatMessages)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);
			PermissionRank senderRank = PermissionRank.GetPermissionRankByOrdinal(membership.Rank);
			ChatUser sender = membership.ChatUser;
			Group group = membership.Group;
			string text = args.Message;

			await Clients.User(Context.UserIdentifier)
				.SendAsync(
					"ReceiveCommandMessage",
					new ReceiveCommandMessageArgs() {
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
					new ReceiveCommandMessageArgs() {
						Color = "FF0000",
						Message = $"ERROR: {splitText[0]} is not a valid or accessible command."
					}
					);
		}

		public async Task SendMessage(SendMessageArgs args) {
			//Load data from database based on args
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
				.ThenInclude(g => g.ChatMessages)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);
			PermissionRank senderRank = PermissionRank.GetPermissionRankByOrdinal(membership.Rank);
			PermissionRank minRank = PermissionRank.GetPermissionRankByOrdinal(args.MinRank);
			ChatUser sender = membership.ChatUser;
			Group group = membership.Group;

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
					new ReceiveMessageArgs() {
						SenderID = sender.Id,
						GroupID = group.GroupID,
						SenderName = sender.UserName,
						SenderRankColor = senderRank.Color,
						Message = args.Message, 
						Timestamp = chatMessage.TimeStamp.ToLocalTime().ToShortTimeString()
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

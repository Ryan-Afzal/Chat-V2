using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
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

		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;

		public ChatHub(UserManager<ChatUser> userManager, ChatContext context) {
			_userManager = userManager;
			_context = context;
		}
		
		public async Task GetPreviousMessages(int groupId, int rankOrd, int prevStart, int prevEnd) {
			var group = GetGroupById(groupId);//Get the actual group object

			//basic test code that just gets every single previous message regardless of prevStart and prevEnd.
			LinkedList<IncomingMessageArgs> list = new LinkedList<IncomingMessageArgs>();
			int i = group.ChatMessages.Count - 1;
			foreach (var m in group.ChatMessages) {
				var user = await _userManager.FindByIdAsync(m.ChatUserID + "");
				var rank = PermissionRank.GetPermissionRankByOrdinal(rankOrd);
				var messageRank = PermissionRank.GetPermissionRankByOrdinal(m.ChatUserRank);//rank of user who sent the message
				
				if (rank.CompareTo(PermissionRank.GetPermissionRankByOrdinal(m.MinRank)) >= 0) {
					if (i < prevEnd) {
						if (i < prevStart) {
							break;
						}
						
						list.AddLast(new IncomingMessageArgs() {
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

		public async Task SendMessage(OutgoingMessageArgs args, string message) {
			//Load data from database based on args
			PermissionRank senderRank = PermissionRank.GetPermissionRankByOrdinal(args.SenderRank);
			PermissionRank minRank = PermissionRank.GetPermissionRankByOrdinal(args.MinRank);
			ChatUser sender = await _userManager.FindByIdAsync(args.SenderID + "");
			Group group = GetGroupById(args.GroupID);



			//Log the message
			ChatMessage chatMessage = new ChatMessage() {
				ChatUserID = sender.Id,
				GroupID = group.GroupID,
				TimeStamp = DateTime.Now,
				Message = message,
				ChatUserRank = senderRank.Ordinal,
				MinRank = minRank.Ordinal
			};
			group.ChatMessages.Add(chatMessage);
			_context.SaveChanges();

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
						Message = message
					});
		}

		private Group GetGroupById(int groupId) {
			var group = _context.Group
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

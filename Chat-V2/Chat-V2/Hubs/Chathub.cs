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

		//TODO Load Previous Messages

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
						SenderRankColor = senderRank.Color
					}, 
					message);
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

		private IReadOnlyList<string> GetUsersInGroup(int groupId, PermissionRank rank) {
			return GetUsersInGroup(GetGroupById(groupId), rank);
		}

		private IReadOnlyList<string> GetUsersInGroup(Group group, PermissionRank rank) {
			List<string> list = new List<string>();

			foreach (var m in group.Memberships) {
				list.Add(m.ChatUserID + "");
			}

			return list.AsReadOnly();
		}

		private IClientProxy GetClientsInGroup(Group group, PermissionRank rank) {
			return Clients.Users(GetUsersInGroup(group, rank));
		}

		private IClientProxy GetClientsInGroup(int groupId, PermissionRank rank) {
			return Clients.Users(GetUsersInGroup(groupId, rank));
		}

	}
}

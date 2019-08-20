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

		public async Task SendMessageAsync(OutgoingMessageArgs args, string message) {
			PermissionRank rank = PermissionRank.GetPermissionRankByOrdinal(args.Rank);

			await GetClientsInGroup(args.GroupID, rank)
				.SendAsync(
					"ReceiveMessage", 
					new IncomingMessageArgs() {
						SenderID = args.SenderID,
						SenderName = (await _userManager.FindByIdAsync(args.SenderID + "")).UserName,
						Rank = rank
					}, 
					message);
		}

		public async Task SendUpdateMessageAsync() {
			throw new NotImplementedException();
		}

		private IReadOnlyList<string> GetUsersInGroup(int groupId, PermissionRank rank) {
			List<string> list = new List<string>();

			var group = _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefault(g => g.GroupID == groupId);

			if (group == null) {
				throw new ArgumentException("Invalid Group ID");//shouldn't happen, but just in case...
			}

			foreach (var m in group.Memberships) {
				list.Add(m.ChatUserID + "");
			}

			return list.AsReadOnly();
		}

		private IClientProxy GetClientsInGroup(int groupId, PermissionRank rank) {
			return Clients.Users(GetUsersInGroup(groupId, rank));
		}

		public async Task SendMessageToAll(string userId, string message) {
			await Clients.All.SendAsync("ReceiveMessage", (await _userManager.FindByIdAsync(userId)).UserName, message);
		}

		public async Task SendMessageToUser(string userId, string message) {
			await Clients.User(userId).SendAsync("ReceiveMessage", message);
		}

	}
}

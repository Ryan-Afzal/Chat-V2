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

		public async Task SendMessage(OutgoingMessageArgs args, string message) {
			PermissionRank rank = PermissionRank.GetPermissionRankByOrdinal(args.Rank);
			ChatUser sender = await _userManager.FindByIdAsync(args.SenderID + "");

			await GetClientsInGroup(args.GroupID, rank)
				.SendAsync(
					"ReceiveMessage", 
					new IncomingMessageArgs() {
						SenderID = args.SenderID,
						SenderName = sender.UserName,
						SenderRankOrdinal = rank.Ordinal,
						SenderRankName = rank.Name,
						SenderRankColor = rank.Color
					}, 
					message);
		}

		public async Task SendUpdateMessage() {
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

	}
}

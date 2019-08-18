using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Hubs {
	public class ChatHub : Hub {

		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;

		public ChatHub(UserManager<ChatUser> userManager, ChatContext context) {
			_userManager = userManager;
			_context = context;
		}

		public async Task SendMessageToAll(string userId, string message) {
			await Clients.All.SendAsync("ReceiveMessage", (await _userManager.FindByIdAsync(userId)).UserName, message);
		}

		public async Task SendMessageToUser(string userId, string message) {
			await Clients.User(userId).SendAsync("ReceiveMessage", message);
		}

	}
}

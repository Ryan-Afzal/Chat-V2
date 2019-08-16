using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Hubs {
	public class ChatHub : Hub {

		public ChatHub() : base() {
			
		}

		public async Task SendMessageToAll(string userId, string message) {
			await Clients.All.SendAsync("ReceiveMessage", userId, message);
		}

		public async Task SendMessageToUser(string userId, string message) {
			await Clients.User(userId).SendAsync("ReceiveMessage", message);
		}

	}
}

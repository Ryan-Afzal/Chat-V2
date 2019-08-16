using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Hubs {
	public class ChatHub : Hub {

		public async Task SendMessage(ChatUser user, string message) {
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

	}
}

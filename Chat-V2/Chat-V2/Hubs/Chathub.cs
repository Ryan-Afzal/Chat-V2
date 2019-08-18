using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Hubs {
	public class ChatHub : Hub {

		private IServiceProvider _serviceProvider;
		private ChatContext _context;

		public ChatHub(IServiceProvider serviceProvider) {
			_serviceProvider = serviceProvider;
			using (var scope = _serviceProvider.CreateScope()) {
				_context = scope.ServiceProvider.GetRequiredService<ChatContext>();
			}
		}

		public async Task SendMessageToAll(string userId, string message) {
			await Clients.All.SendAsync("ReceiveMessage", userId, message);
		}

		public async Task SendMessageToUser(string userId, string message) {
			await Clients.User(userId).SendAsync("ReceiveMessage", message);
		}

	}
}

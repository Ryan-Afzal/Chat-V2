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
using System.Text;
using System.Threading.Tasks;

namespace Chat_V2.Hubs {

	/// <summary>
	/// Manages Notifications. Sends a NewNotification event when a notification is added to the user
	/// </summary>
	public class NotifHub : Hub {

		public UserManager<ChatUser> UserManager { get; }
		public ChatContext ChatContext { get; }

		public NotifHub(UserManager<ChatUser> userManager, ChatContext context) {
			UserManager = userManager;
			ChatContext = context;
		}

		

	}
}

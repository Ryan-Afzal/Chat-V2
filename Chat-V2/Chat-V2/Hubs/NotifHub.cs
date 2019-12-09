﻿using Chat_V2.Areas.Identity.Data;
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

		public async Task AddNotification(AddNotificationArgs args) {
			if ((args.ChatUserID + "") != Context.UserIdentifier) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			var chatUser = await ChatContext.Users
				.Include(u => u.Notifications)
				.FirstOrDefaultAsync(u => u.Id == args.ChatUserID);

			if (chatUser == null) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			Notification notif = new Notification() {
				ChatUserID = args.ChatUserID,
				Date = DateTime.Now,
				Text = args.Text,
				ViewURL = args.ViewURL
			};

			chatUser.Notifications.Add(notif);

			IClientProxy proxy = Clients.User(args.ChatUserID + "");

			await proxy.SendAsync("NewNotification", 
				new NewNotificationArgs() {
					ChatUserID = args.ChatUserID
				});
		}

		public async Task RemoveNotification(RemoveNotificationArgs args) {
			if ((args.ChatUserID + "") != Context.UserIdentifier) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			var chatUser = await ChatContext.Users
				.Include(u => u.Notifications)
				.FirstOrDefaultAsync(u => u.Id == args.ChatUserID);

			if (chatUser == null) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			chatUser.Notifications.Remove(chatUser.Notifications.FirstOrDefault(n => n.NotificationID == args.NotificationID));
		}

		public async Task GetNotifications(GetNotificationsArgs args) {
			if ((args.ChatUserID + "") != Context.UserIdentifier) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			var chatUser = await ChatContext.Users
				.Include(u => u.Notifications)
				.FirstOrDefaultAsync(u => u.Id == args.ChatUserID);

			if (chatUser == null) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			throw new NotImplementedException();
		}

	}
}

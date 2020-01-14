using Chat_V2.Areas.Identity.Data;
using Chat_V2.Extensions;
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

		public override async Task OnConnectedAsync() {
			await base.OnConnectedAsync();

			int id = int.Parse(Context.UserIdentifier);
			var user = await UserManager.GetUserAsync(Context.User);
			bool hasNew = await ChatContext.Notification
				.AnyAsync(n => n.ChatUserID == user.Id)
				|| await ChatContext.PersonalChatInvitation
				.AnyAsync(i => i.ChatUserID == user.Id)
				|| await ChatContext.GroupJoinInvitation
				.AnyAsync(i => i.ChatUserID == user.Id);

			if (hasNew) {
				IClientProxy proxy = Clients.User(Context.UserIdentifier);

				await proxy.SendAsync("NewNotification",
					new NewNotificationArgs() {
						ChatUserID = user.Id
					});
			}
		}

		public async Task AddNotification(AddNotificationArgs args) {
			var chatUser = await ChatContext.Users
				.FirstOrDefaultAsync(u => u.Id == args.ChatUserID);

			if (chatUser == null) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			Notification notif = new Notification() {
				ChatUserID = args.ChatUserID,
				Date = DateTime.Now,
				Title = args.Title,
				Text = args.Text,
				ViewURL = args.ViewURL
			};

			await ChatContext.Notification.AddAsync(notif);
			await ChatContext.SaveChangesAsync();

			await this.SendNotificationAsync(notif);
		}

		public async Task RemoveNotification(RemoveNotificationArgs args) {
			if ((args.ChatUserID + "") != Context.UserIdentifier) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			var chatUser = await ChatContext.Users
				.FirstOrDefaultAsync(u => u.Id == args.ChatUserID);

			if (chatUser == null) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			var notification = await ChatContext.Notification
				.FirstOrDefaultAsync(n => n.NotificationID == args.NotificationID);

			ChatContext.Notification.Remove(notification);
			await ChatContext.SaveChangesAsync();
		}

		public async Task GetNotifications(GetNotificationsArgs args) {
			if ((args.ChatUserID + "") != Context.UserIdentifier) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			var chatUser = await ChatContext.Users
				.FirstOrDefaultAsync(u => u.Id == args.ChatUserID);

			if (chatUser == null) {
				throw new ArgumentException(nameof(args.ChatUserID));
			}

			await Clients.Caller.SendAsync(
				"ReceiveNotifications",
				await ChatContext.Notification
					.Where(n => n.ChatUserID == chatUser.Id)
					.Select(n => new ReceiveNotificationArgs(n))
					.ToListAsync()
			);
		}

		public async Task GetNumNewMessages() {
			var id = int.Parse(Context.UserIdentifier);
			var chatUser = await ChatContext.Users
				.Include(u => u.Memberships)
				.FirstOrDefaultAsync(u => u.Id == id);

			var memberships = chatUser.Memberships.Where(m => m.NumNew > 0);
			int sum = 0;

			foreach (var m in memberships) {
				sum += m.NumNew;
			}

			await Clients.Caller.SendAsync("ReceiveNumNewMessages", new ReceiveNumNewMessagesArgs() { ChatUserID = chatUser.Id, Num = sum });
		}

	}
}

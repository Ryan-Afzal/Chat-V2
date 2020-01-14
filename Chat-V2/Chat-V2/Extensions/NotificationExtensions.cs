using Chat_V2.Hubs;
using Chat_V2.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Extensions {
	public static class NotificationExtensions {

		public static async Task SendNotificationAsync(this NotifHub hub, Notification notif) {
			await SendNotificationAsync(hub.Clients.User(notif.ChatUserID.ToString()), notif);
		}

		public static async Task SendNotificationAsync(this IHubContext<NotifHub> context, Notification notif) {
			await SendNotificationAsync(context.Clients.User(notif.ChatUserID.ToString()), notif);
		}

		private static async Task SendNotificationAsync(IClientProxy proxy, Notification notif) {
			await proxy.SendAsync("NewNotification",
				new NewNotificationArgs() {
					ChatUserID = notif.ChatUserID
				});

			await proxy.SendAsync("ReceiveNotification",
				new ReceiveNotificationArgs(notif));
		}

		public static async Task SendNewNotificationAsync(this IHubContext<NotifHub> context, int chatUserID) {
			await context.Clients.User(chatUserID.ToString()).SendAsync("NewNotification",
				new NewNotificationArgs() {
					ChatUserID = chatUserID
				});
		}

	}
}

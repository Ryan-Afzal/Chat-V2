using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class GetUserListCommand : Command {

		public GetUserListCommand() : base(0, "members-list", "Lists all group memebers.", "members-list") { }

		public async override Task Execute(CommandArgs args) {
			IClientProxy proxy = args.Hub.Clients.User($"{args.User.Id}");

			await proxy.SendAsync("ReceiveCommandMessage",
				new ReceiveCommandMessageArgs() {
					Color = "0000FF",
					Message = $"Members of group {args.Group.Name}#{args.Group.GroupID}"
				});
			await proxy.SendAsync("ReceiveCommandMessage",
				new ReceiveCommandMessageArgs() {
					Color = "0000FF",
					Message = $"ID       Name              Rank            Online  "
				});

			var list = from membership in args.Group.Memberships
					   where membership.IsOnlineInGroup
					   select new ReceiveCommandMessageArgs() {
						   Color = "0000FF",
						   Message = $"{(membership.ChatUserID + "").PadRight(7)}  {args.Hub.ChatContext.Users.FirstOrDefault(u => u.Id == membership.ChatUserID).UserName.PadRight(16).Substring(0, 16)}  {PermissionRank.GetPermissionRankByOrdinal(membership.Rank).Name.PadRight(14)}  {membership.IsOnlineInGroup}"
					   };

			foreach (var msg in list) {
				await proxy.SendAsync("ReceiveCommandMessage", msg);
			}
		}
	}
}

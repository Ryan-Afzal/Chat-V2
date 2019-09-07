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
				new IncomingCommandMessageArgs() {
					Color = "0000FF",
					Message = $"Members of group {args.Group.Name}#{args.Group.GroupID}"
				});
			await proxy.SendAsync("ReceiveCommandMessage",
				new IncomingCommandMessageArgs() {
					Color = "0000FF",
					Message = $"ID       Name              Rank            Online  "
				});

			foreach (Membership membership in args.Group.Memberships) {
				await proxy.SendAsync("ReceiveCommandMessage",
					new IncomingCommandMessageArgs() {
						Color = "0000FF",
						Message = $"{(membership.ChatUserID + "").PadRight(7)}  {membership.ChatUser.UserName.PadRight(16).Substring(0, 16)}  {PermissionRank.GetPermissionRankByOrdinal(membership.Rank).Name.PadRight(14)}  {membership.IsActive}"
					});
			}
		}
	}
}

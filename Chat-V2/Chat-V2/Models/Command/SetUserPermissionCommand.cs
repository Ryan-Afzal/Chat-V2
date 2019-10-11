using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class SetUserPermissionCommand : Command {

		public SetUserPermissionCommand() : base(1, "set-permission", "Sets the permission of the specified user.", "set-permission <userId> <newRank>") { }

		public async override Task Execute(CommandArgs args) {
			int userId = int.Parse(args.Args[0]);
			PermissionRank rank = PermissionRank.GetPermissionRankByName(args.Args[1]);

			IClientProxy proxy = args.Hub.Clients.User($"{args.User.Id}");

			if (args.UserRank.Ordinal > rank.Ordinal) {//If the user is capable of changing a user to this rank
				//Check if the specified user exists, and if they are of higher rank
				ChatUser user = args.Hub.ChatContext.Users
					.Include(u => u.Memberships)
					.FirstOrDefault(u => u.Id == userId);

				if (user == null) {
					await proxy.SendAsync("ReceiveCommandMessage",
						new ReceiveCommandMessageArgs() {
							Color = "FF0000",
							Message = $"ERROR: The specified user does not exist."
						});
				} else {
					var membership = user.Memberships
					.FirstOrDefault(m => m.GroupID == args.Group.GroupID);

					if (args.UserRank.Ordinal <= membership.Rank) {
						await proxy.SendAsync("ReceiveCommandMessage",
							new ReceiveCommandMessageArgs() {
								Color = "FF0000",
								Message = $"ERROR: You do not have permission to change this user's rank."
							});

						return;
					} else {
						var oldRank = PermissionRank.GetPermissionRankByOrdinal(membership.Rank);
						membership.Rank = rank.Ordinal;
						await args.Hub.ChatContext.SaveChangesAsync();

						await proxy.SendAsync("ReceiveCommandMessage",
							new ReceiveCommandMessageArgs() {
								Color = "0000FF",
								Message = $"User #{user.Id}({user.UserName})'s rank successfully updated from {oldRank.Name} to {rank.Name}."
							});
					}
				}
			} else {
				await proxy.SendAsync("ReceiveCommandMessage",
					new ReceiveCommandMessageArgs() {
						Color = "FF0000",
						Message = $"ERROR: You do not have permission to change this user's rank."
					});
			}
		}

	}
}

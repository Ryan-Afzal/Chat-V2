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
			string userIdString = args.Args[0];
			int userId = int.Parse(userIdString);
			PermissionRank rank = PermissionRank.GetPermissionRankByName(args.Args[1]);

			if (args.UserRank.Ordinal > rank.Ordinal) {//If the user is capable of changing a user to this rank
				//Check if the specified user exists, and if they are of higher rank
				ChatUser user = args.Hub.ChatContext.Users
					.Include(u => u.Memberships)
					.FirstOrDefault(u => u.Id == userId);

				if (user == null) {
					throw new NotImplementedException();
				}

				throw new NotImplementedException();
			}

			throw new NotImplementedException();
		}

	}
}

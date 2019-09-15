using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class SetUserPermissionCommand : Command {

		public SetUserPermissionCommand() : base(1, "set-permission", "Sets the permission of the specified user.", "set-permission <userId> <newRank>") { }

		public async override Task Execute(CommandArgs args) {
			throw new NotImplementedException();
		}

	}
}

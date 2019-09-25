using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class CreateGroupCommand : Command {

		public CreateGroupCommand() : base(0, "create-group", "Creates a new group", "create-group <groupName>") { }

		public override Task Execute(CommandArgs args) {
			throw new NotImplementedException();
		}
	}
}

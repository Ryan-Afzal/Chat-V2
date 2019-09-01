using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class HelpCommand : Command {

		public HelpCommand() : base(0, "help", "Lists all accessible commands.", "help") { }

		public override void Execute(CommandArgs args) {
			StringBuilder builder = new StringBuilder();

			builder.Append($"Commands available at rank: {args.UserRank.Name}");

			foreach (ICommand command in args.Hub.ChatContext.CommandList.GetCommandsAtRank(args.UserRank.Ordinal)) {
				builder.Append($"       {command.Usage}: {command.Description}");
			}

			args.Hub.Clients.User($"{args.User.Id}")
				.SendAsync(
				"ReceiveCommandMessage",
				new IncomingCommandMessageArgs() {
					Color = "0000FF",
					Message = builder.ToString()
				}
				);
		}
	}
}

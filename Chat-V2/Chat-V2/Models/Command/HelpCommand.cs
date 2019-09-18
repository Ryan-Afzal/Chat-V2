using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class HelpCommand : Command {

		public HelpCommand() : base(0, "help", "Lists all accessible commands.", "help") { }

		public async override Task Execute(CommandArgs args) {
			IClientProxy proxy = args.Hub.Clients.User($"{args.User.Id}");

			await proxy.SendAsync("ReceiveCommandMessage",
				new ReceiveCommandMessageArgs() {
					Color = "0000FF",
					Message = $"Commands available at rank: {args.UserRank.Name}"
				});

			foreach (ICommand command in args.Hub.ChatContext.CommandList.GetCommandsAtRank(args.UserRank.Ordinal)) {
				await proxy.SendAsync("ReceiveCommandMessage",
					new ReceiveCommandMessageArgs() {
						Color = "0000FF",
						Message = $"    [{command.Usage}]: {command.Description}"
					});
			}

			
				
		}
	}
}

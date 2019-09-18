using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class GroupDataCommand : Command {

		public GroupDataCommand() : base(0, "group-data", "Gets data about the current group.", "group-data") { }

		public async override Task Execute(CommandArgs args) {
			IClientProxy proxy = args.Hub.Clients.User($"{args.User.Id}");

			await proxy.SendAsync("ReceiveCommandMessage",
				new ReceiveCommandMessageArgs() {
					Color = "0000FF",
					Message = $"Data for group #{args.Group.GroupID}:"
				});

			await proxy.SendAsync("ReceiveCommandMessage",
				new ReceiveCommandMessageArgs() {
					Color = "0000FF",
					Message = $"Name: {args.Group.Name}"
				});


			await proxy.SendAsync("ReceiveCommandMessage",
				new ReceiveCommandMessageArgs() {
					Color = "0000FF",
					Message = $"Date Created: UNAVAILABLE"
				});

			await proxy.SendAsync("ReceiveCommandMessage",
				new ReceiveCommandMessageArgs() {
					Color = "0000FF",
					Message = $"Members: {args.Group.Memberships.Count}"
				});
		}
	}
}

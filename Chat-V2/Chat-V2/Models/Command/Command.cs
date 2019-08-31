using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Chat_V2.Models.Command {
	public abstract class Command : ICommand {

		public static readonly char CommandCharacter = '$';

		public Command(int minRank, string name, string desc) {
			MinRank = minRank;
			Name = name;
			Description = desc;
		}

		public int MinRank { get; }
		public string Name { get; }
		public string Description { get; }

		public abstract void Execute(UserManager<ChatUser> userManager, ChatContext context);

	}
}

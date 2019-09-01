using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public interface ICommand {
		void Execute(CommandArgs args);

		int MinRank { get; }
		string Name { get; }
		string Description { get; }
	}
}

using Chat_V2.Areas.Identity.Data;
using Chat_V2.Hubs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class CommandArgs {
		public ChatHub Hub { get; set; }
		public Group Group { get; set; }
		public ChatUser User { get; set; }
		public PermissionRank UserRank { get; set; }
		public string[] Args { get; set; }
	}
}

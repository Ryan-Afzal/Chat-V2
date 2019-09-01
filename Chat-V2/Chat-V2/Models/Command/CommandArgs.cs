using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models.Command {
	public class CommandArgs {
		public UserManager<ChatUser> UserManager { get; set; }
		public ChatContext Context { get; set; }
		public Group Group { get; set; }
		public ChatUser User { get; set; }
		public PermissionRank UserRank { get; set; }
		public string[] Args { get; set; }
	}
}

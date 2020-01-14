using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class MultiuserGroup : Group {
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsPrivate { get; set; }
		public bool IsArchived { get; set; }
		public string GroupImage { get; set; }
		public int NumOnline { get; set; }

		public ICollection<ChatUser> BannedUsers { get; set; }
		public ICollection<GroupJoinRequest> GroupJoinRequests { get; set; }
	}
}

using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	/// <summary>
	/// Stores data about a <code>User</code>'s relationship to a <code>Group</code>.
	/// </summary>
	public class Membership {
		public int MembershipID { get; set; }
		public int GroupID { get; set; }
		public int ChatUserID { get; set; }
		public int Rank { get; set; }
		public bool IsOnlineInGroup { get; set; }
		public bool IsActiveInGroup { get; set; }
		public int? LastViewedMessageID { get; set; }
		public int NumNew { get; set; }

		public Group Group { get; set; }
		public ChatUser ChatUser { get; set; }
	}
}

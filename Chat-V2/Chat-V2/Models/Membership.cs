using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	/// <summary>
	/// Stores data about a <see cref="Chat_V2.Areas.Identity.Data.ChatUser"/>'s relationship to a <see cref="Chat_V2.Models.Group"/>.
	/// </summary>
	public abstract class Membership {
		[Key]
		public int MembershipID { get; set; }
		public int GroupID { get; set; }
		public int ChatUserID { get; set; }
		public bool IsOnlineInGroup { get; set; }
		public bool IsActiveInGroup { get; set; }
		public int? LastViewedMessageID { get; set; }
		public int NumNew { get; set; }

		public Group Group { get; set; }
		public ChatUser ChatUser { get; set; }
	}
}

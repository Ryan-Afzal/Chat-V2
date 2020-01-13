using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class GroupJoinInvitation {
		[Key]
		public int GroupJoinInvitationID { get; set; }
		public int GroupID { get; set; }
		public int ChatUserID { get; set; }
		public string Message { get; set; }
		public DateTime DateSent { get; set; }

		public MultiuserGroup Group { get; set; }
		public ChatUser ChatUser { get; set; }
	}
}

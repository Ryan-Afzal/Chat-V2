using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class PersonalChatInvitation {
		[Key]
		public int PersonalChatInvitationID { get; set; }
		public int ChatUserID { get; set; }
		public int SenderID { get; set; }
		public string Message { get; set; }
		public DateTime DateSent { get; set; }

		public ChatUser ChatUser { get; set; }
	}
}

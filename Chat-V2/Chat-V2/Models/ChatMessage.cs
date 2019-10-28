using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class ChatMessage {
		public int ChatMessageID { get; set; }
		public int GroupID { get; set; }
		public int ChatUserID { get; set; }
		public string ChatUserName { get; set; }//Used only when getting the name of a terminated user
		public int ChatUserRank { get; set; }
		public int MinRank { get; set; }
		public DateTime TimeStamp { get; set; }
		public MembershipStatusType StatusType { get; set; }
		public string Message { get; set; }//The body of the message, not including the 'message-content' div
		
		public Group Group { get; set; }
		public ChatUser ChatUser { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	/// <summary>
	/// Represents a group in the chat system.
	/// </summary>
	public class Group {
		public int GroupID { get; set; }
		public string Name { get; set; }
		public DateTime DateCreated { get; set; }
		/// <summary>
		/// Private groups are hidden from public view, and can only be joined with an invitation, one cannot send join requests.
		/// </summary>
		public bool IsPrivate { get; set; }

		public ICollection<ChatMessage> ChatMessages { get; set; }
		public ICollection<Membership> Memberships { get; set; }
		public ICollection<GroupJoinRequest> GroupJoinRequests { get; set; }
	}
}

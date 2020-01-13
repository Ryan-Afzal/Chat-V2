using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	/// <summary>
	/// Represents a group in the chat system.
	/// </summary>
	public abstract class Group {
		[Key]
		public int GroupID { get; set; }
		public DateTime DateCreated { get; set; }

		public ICollection<ChatMessage> ChatMessages { get; set; }
		public ICollection<Membership> Memberships { get; set; }
	}
}

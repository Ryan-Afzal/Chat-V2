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
	public class Group {
		[Key]
		public int GroupID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Date Created")]
		public DateTime DateCreated { get; set; }
		[Display(Name = "Private?")]
		public bool IsPrivate { get; set; }
		public bool IsArchived { get; set; }
		public string GroupImage { get; set; }

		public ICollection<ChatUser> BannedUsers { get; set; }

		public ICollection<ChatMessage> ChatMessages { get; set; }
		public ICollection<Membership> Memberships { get; set; }
		public ICollection<GroupJoinRequest> GroupJoinRequests { get; set; }
	}
}

﻿using System;
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
		/// <summary>
		/// Private groups are hidden from public view, and can only be joined with an invitation, one cannot send join requests.
		/// </summary>
		[Display(Name = "Private?")]
		public bool IsPrivate { get; set; }
		[Dipslay(Name = "Archived?")]
		public bool IsArchived { get; set; }

		public ICollection<int> BannedIDs { get; set; }

		public ICollection<ChatMessage> ChatMessages { get; set; }
		public ICollection<Membership> Memberships { get; set; }
		public ICollection<GroupJoinRequest> GroupJoinRequests { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chat_V2.Models;
using Microsoft.AspNetCore.Identity;

namespace Chat_V2.Areas.Identity.Data {

	public class ChatUser : IdentityUser<int> {

		[PersonalData]
		public string FirstName { get; set; }

		[PersonalData]
		public string LastName { get; set; }

		public string ProfileDescription { get; set; }
		
        public bool IsOnline {
			get {
				return NumOnline > 0;
			}
		}

		public int NumOnline { get; set; }

		public int ProfileImageID { get; set; }

		public ICollection<Membership> Memberships { get; set; }
		public ICollection<GroupJoinInvitation> GroupJoinInvitations { get; set; }

		public AppImage ProfileImage { get; set; }

	}

	public class ChatRole : IdentityRole<int> { }
}

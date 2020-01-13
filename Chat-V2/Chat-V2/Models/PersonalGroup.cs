using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class PersonalGroup : Group {

		[NotMapped]
		public IQueryable<PersonalGroupMembership> PersonalGroupMemberships { get => Memberships.AsQueryable().OfType<PersonalGroupMembership>(); }

		public ChatUser GetOtherUser(ChatUser thisUser) {
			return PersonalGroupMemberships
				.FirstOrDefault(m => m.ChatUserID != thisUser.Id).ChatUser;
		}

	}
}

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

		[PersonalData]
		public string ProfileDescription { get; set; }
		
        public bool IsOnline {
			get {
				return NumOnline > 0;
			}
		}

		public int NumOnline { get; set; }

		public string ProfileImage { get; set; }

		public bool IsEnabled { get; set; }



		public ICollection<Membership> Memberships { get; set; }
		public ICollection<GroupJoinInvitation> GroupJoinInvitations { get; set; }
		public ICollection<PersonalChatInvitation> PersonalChatInvitations { get; set; }
		public ICollection<Notification> Notifications { get; set; }

		/// <summary>
		/// Soft-deletes the user.
		/// </summary>
		/// <returns></returns>
		public async Task<bool> DeleteAsync(ChatContext chatContext) {
			FirstName = null;
			LastName = null;
			ProfileDescription = null;

			IsEnabled = false;

			foreach (Membership m in Memberships) {
				chatContext.Remove(m);
			}

			foreach (GroupJoinInvitation i in GroupJoinInvitations) {
				chatContext.Remove(i);
			}

			foreach (PersonalChatInvitation i in PersonalChatInvitations) {
				chatContext.Remove(i);
			}

			foreach (Notification n in Notifications) {
				chatContext.Remove(n);
			}

			await chatContext.SaveChangesAsync();

			return true;
		}

	}

	public class ChatRole : IdentityRole<int> { }
}

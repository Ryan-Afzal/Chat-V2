using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Chat_V2.Areas.Identity.Data {

	public class ChatUser : IdentityUser<int> {

		[PersonalData]
		public string FirstName { get; set; }

		[PersonalData]
		public string LastName { get; set; }

	}
}

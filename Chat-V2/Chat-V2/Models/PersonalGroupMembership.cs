using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class PersonalGroupMembership : Membership {
		[NotMapped]
		public PersonalGroup PersonalGroup { get => (PersonalGroup)Group; }
	}
}

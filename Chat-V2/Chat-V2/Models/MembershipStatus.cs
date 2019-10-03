using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class MembershipStatus {
		[Key]
		public int MembershipID { get; set; }
		public MembershipStatusType Type { get; set; }
		public DateTime DateIssued { get; set; }
		public DateTime Expiration { get; set; }
	}

	public enum MembershipStatusType {
		NONE,
		CENSURE,
		BLOCK
	}
}

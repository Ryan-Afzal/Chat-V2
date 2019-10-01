using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class MembershipStatus {

		public enum StatusType {
			CENSURE,
			BLOCK
		}

		[Key]
		public int MembershipID { get; set; }
		public StatusType Type { get; set; }
		public DateTime DateIssued { get; set; }
		public DateTime Expiration { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class Membership {
		public int MembershipID { get; set; }
		public int UserID { get; set; }
		public int GroupID { get; set; }
	}
}

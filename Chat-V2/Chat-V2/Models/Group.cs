using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class Group {
		public int GroupID { get; set; }
		public string Name { get; set; }
		
		public ICollection<Membership> Memberships { get; set; }
	}
}

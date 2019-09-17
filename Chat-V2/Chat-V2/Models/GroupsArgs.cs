using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	
	public class UserConnectedGroupsArgs {
		public int UserID { get; set; }
	}

	public class UserDisconnectedGroupsArgs {
		public int UserID { get; set; }
	}

}

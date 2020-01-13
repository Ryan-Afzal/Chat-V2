using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class MultiuserGroupMembership : Membership {
		public int Rank { get; set; }

		[NotMapped]
		public MultiuserGroup MultiuserGroup { get => (MultiuserGroup)Group; }
	}
}

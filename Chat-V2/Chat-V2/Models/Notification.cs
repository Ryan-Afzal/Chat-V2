using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class Notification {
		[Key]
		public int ChatUserID { get; set; }
		public string FormattedText { get; set; }
	}
}

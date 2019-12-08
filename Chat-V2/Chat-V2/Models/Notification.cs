using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class Notification {
		[Key]
		public int NotificationID { get; set; }
		public int ChatUserID { get; set; }
		public DateTime Date { get; set; }
		public string Text { get; set; }
		public string ViewURL { get; set; }
	}
}

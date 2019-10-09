using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class Image {
		[Key]
		public int ChatUserID { get; set; }
		public byte[] Data { get; set; }

		public ChatUser ChatUser { get; set; }
	}
}

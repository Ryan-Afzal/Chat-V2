using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class ProfileImage {
		public int ProfileImageID { get; set; }
		public byte[] Data { get; set; }
		public string ContentType { get; set; }
	}
}

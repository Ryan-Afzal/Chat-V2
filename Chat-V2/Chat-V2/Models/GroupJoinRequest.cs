﻿using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class GroupJoinRequest {
		public int RequestID { get; set; }
		public int GroupID { get; set; }
		public int ChatUserID { get; set; }
		public DateTime DateSent { get; set; }

		public Group Group { get; set; }
		public ChatUser ChatUser { get; set; }
	}
}

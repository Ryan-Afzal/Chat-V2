﻿using Chat_V2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {
	public class ChatMessage {
		[Key]
		public int ChatMessageID { get; set; }
		public int GroupID { get; set; }
		public int ChatUserID { get; set; }
		public DateTime Timestamp { get; set; }
		public string Message { get; set; }
		public string Multimedia { get; set; }
		
		public Group Group { get; set; }
		public ChatUser ChatUser { get; set; }
	}
}

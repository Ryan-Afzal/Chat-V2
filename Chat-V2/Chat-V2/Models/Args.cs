using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {

	public class OutgoingMessageArgs {
		public int GroupID { get; set; }
		public int SenderID { get; set; }
		public int Rank { get; set; }
	}

	public class IncomingMessageArgs {
		public int SenderID { get; set; }
		public string SenderName { get; set; }
		public int SenderRankOrdinal { get; set; }
		public string SenderRankName { get; set; }
		public string SenderRankColor { get; set; }
	}

}

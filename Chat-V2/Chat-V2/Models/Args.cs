using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {

	/// <summary>
	/// Represents the arguments passed from the client to the server when sending a message.
	/// </summary>
	public class OutgoingMessageArgs {
		public int GroupID { get; set; }
		public int SenderID { get; set; }
		public int SenderRank { get; set; }
		public int MinRank { get; set; }
		public string Message { get; set; }
	}

	/// <summary>
	/// Represents the arguments passed from the server to the client when receiving a message.
	/// </summary>
	public class IncomingMessageArgs {
		public int SenderID { get; set; }
		public string SenderName { get; set; }
		public int SenderRankOrdinal { get; set; }
		public string SenderRankName { get; set; }
		public string SenderRankColor { get; set; }
		public string Message { get; set; }
	}

	/// <summary>
	/// Represents a command string being sent from the client to the server.
	/// </summary>
	public class OutgoingCommandArgs {
		public int GroupID { get; set; }
		public int SenderID { get; set; }
		public int SenderRank { get; set; }
		public string Text { get; set; }
	}
	
	/// <summary>
	/// Represents command output messages coming in from the server.
	/// </summary>
	public class IncomingCommandMessageArgs {
		public string Color { get; set; }
		public string Message { get; set; }
	}

	/// <summary>
	/// Represents data passed to the server when a client connects.
	/// </summary>
	public class ClientConnectedArgs {
		public int GroupID { get; set; }
		public int SenderID { get; set; }
	}

	/// <summary>
	/// Represents data passed to the server when a client disconnects.
	/// </summary>
	public class ClientDisconnectedArgs {
		public int GroupID { get; set; }
		public int SenderID { get; set; }
	}

	/// <summary>
	/// Represents data passed to the client when a client connects.
	/// </summary>
	public class UserConnectedArgs {
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string UserRankName { get; set; }
	}

	/// <summary>
	/// Represents data passed to the client when a client disconnects.
	/// </summary>
	public class UserDisconnectedArgs {
		public int UserID { get; set; }
	}

}

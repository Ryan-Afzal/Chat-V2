using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {

	//To Server

    public class ConnectedArgs {
        public int UserID { get; set; }
    }

    public class DisconnectedArgs {
        public int UserID { get; set; }
    }

    public class ConnectedToGroupArgs {
        public int MembershipID { get; set; }
    }

    public class DisconnectedFromGroupArgs {
        public int MembershipID { get; set; }
    }

    public class SendMessageArgs {
        public int MembershipID { get; set; }
        public int MinRank { get; set; }
        public string Message { get; set; }
    }

    public class GetPreviousMessagesArgs {
        public int MembershipID { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }

    public class ProcessCommandArgs {
        public int MembershipID { get; set; }
        public string Message { get; set; }
    }

	//To Client

    public class AddGroupArgs {
        public int GroupID { get; set; }
		public int MembershipID { get; set; }
        public string GroupName { get; set; }
    }

    public class RemoveGroupArgs {
        public int GroupID { get; set; }
    }

    public class OtherUserConnectedToGroupArgs {
        public int GroupID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserRank { get; set; }
    }

    public class OtherUserDisconnectedFromGroupArgs {
        public int GroupID { get; set; }
        public int UserID { get; set; }
    }

    public class ReceiveMessageArgs {
        public int SenderID { get; set; }
        public int GroupID { get; set; }
        public string SenderName { get; set; }
        public string SenderRankColor { get; set; }
		public string Message { get; set; }
    }

    public class ReceiveCommandMessageArgs {
        public string Message { get; set; }
        public string Color { get; set; }
    }

}

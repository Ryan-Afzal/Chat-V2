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

	public class ActiveInGroupArgs {
		public int MembershipID { get; set; }
	}

	public class InactiveInGroupArgs {
		public int MembershipID { get; set; }
	}

	public class UserTypingArgs {
		public int MembershipID { get; set; }
	}

	public class UserNotTypingArgs {
		public int MembershipID { get; set; }
	}

	public class SendMessageArgs {
		public int MembershipID { get; set; }
		public int MinRank { get; set; }
		public string Message { get; set; }
	}

	public class GetPreviousMessagesArgs {
		public int MembershipID { get; set; }
		/// <summary>
		/// Index relative to the most recent message
		/// </summary>
		public int StartIndex { get; set; }
		/// <summary>
		/// Number of messages to get
		/// </summary>
		public int Count { get; set; }
	}

	//To Client

	public class AddGroupArgs {
		public int GroupID { get; set; }
		public int MembershipID { get; set; }
		public bool HasNew { get; set; }
		public string GroupName { get; set; }
		public string GroupImage { get; set; }
	}

	public class RemoveGroupArgs {
		public int GroupID { get; set; }
	}

	public class OtherUserConnectedToGroupArgs {
		public int GroupID { get; set; }
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string UserImage { get; set; }
		public string UserRank { get; set; }
	}

	public class OtherUserDisconnectedFromGroupArgs {
		public int GroupID { get; set; }
		public int UserID { get; set; }
	}

	public class OtherUserActiveInGroupArgs {
		public int GroupID { get; set; }
		public int UserID { get; set; }
	}

	public class OtherUserInactiveInGroupArgs {
		public int GroupID { get; set; }
		public int UserID { get; set; }
	}

	public class OtherUserTypingArgs {
		public int UserID { get; set; }
		public int GroupID { get; set; }
		public string UserProfileImage { get; set; }
	}

	public class OtherUserNotTypingArgs {
		public int UserID { get; set; }
		public int GroupID { get; set; }
	}

	public class OtherUserAddedToGroupArgs {
		public int GroupID { get; set; }
	}

	public class OtherUserRemovedFromGroupArgs {
		public int UserID { get; set; }
		public int GroupID { get; set; }
	}

	public class ReceiveGroupDataArgs {
		public int GroupID { get; set; }
		public string GroupName { get; set; }
		public int NumUsers { get; set; }
	}

	public class ReceiveMessageArgs {
        public int SenderID { get; set; }
        public int GroupID { get; set; }
		public string UserName { get; set; }
		public string UserImage { get; set; }
		public string Timestamp { get; set; }
		public string Message { get; set; }
    }

	public class ClientBannedFromGroupArgs {
		public int GroupID { get; set; }
	}

	public class ClientBlockedFromGroupArgs {
		public int GroupID { get; set; }
	}

	public class ClientUnblockedFromGroupArgs {
		public int GroupID { get; set; }
	}

	//Notification
	
	public class AddNotificationArgs {
		public int ChatUserID { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public string ViewURL { get; set; }
	}

	public class RemoveNotificationArgs {
		public int ChatUserID { get; set; }
		public int NotificationID { get; set; }
	}

	public class GetNotificationsArgs {
		public int ChatUserID { get; set; }
	}



	public class NewNotificationArgs {
		public int ChatUserID { get; set; }
	}

	public class ReceiveNotificationArgs {
		public int ChatUserID { get; set; }
		public int NotificationID { get; set; }
		public string Date { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public string ViewURL { get; set; }
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Models {

	//To Server

	public class ConnectedArgs {
		/// <summary>
		/// The ID of the connecting user
		/// </summary>
		public int UserID { get; set; }
	}

	public class DisconnectedArgs {
		/// <summary>
		/// The ID of the disconnecting user
		/// </summary>
		public int UserID { get; set; }
	}

	public class ConnectedToGroupArgs {
		/// <summary>
		/// The ID of the membership used by the connecting user
		/// </summary>
		public int MembershipID { get; set; }
	}

	public class DisconnectedFromGroupArgs {
		/// <summary>
		/// The ID of the membership used by the disconnecting user
		/// </summary>
		public int MembershipID { get; set; }
	}

	public class ActiveInGroupArgs {
		/// <summary>
		/// The ID of the membership used by the calling user
		/// </summary>
		public int MembershipID { get; set; }
	}

	public class InactiveInGroupArgs {
		/// <summary>
		/// The ID of the membership used by the calling user
		/// </summary>
		public int MembershipID { get; set; }
	}

	public class UserTypingArgs {
		/// <summary>
		/// The ID of the membership used by the calling user
		/// </summary>
		public int MembershipID { get; set; }
	}

	public class UserNotTypingArgs {
		/// <summary>
		/// The ID of the membership used by the calling user
		/// </summary>
		public int MembershipID { get; set; }
	}

	public class SendMessageArgs {
		/// <summary>
		/// The ID of the membership used by the calling user
		/// </summary>
		public int MembershipID { get; set; }
		/// <summary>
		/// The minimum rank needed to view the message
		/// </summary>
		public int MinRank { get; set; }
		/// <summary>
		/// The message
		/// </summary>
		public string Message { get; set; }
	}

	public class GetPreviousMessagesArgs {
		/// <summary>
		/// The ID of the membership used by the calling user
		/// </summary>
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
		/// <summary>
		/// The ID of the group
		/// </summary>
		public int GroupID { get; set; }
		/// <summary>
		/// The ID of the membership belonging to the group
		/// </summary>
		public int MembershipID { get; set; }
		/// <summary>
		/// Whether the user has unread messages
		/// </summary>
		public int NumNew { get; set; }
		/// <summary>
		/// The group name
		/// </summary>
		public string GroupName { get; set; }
		/// <summary>
		/// The group image
		/// </summary>
		public string GroupImage { get; set; }
	}

	public class RemoveGroupArgs {
		/// <summary>
		/// The ID of the group to remove
		/// </summary>
		public int GroupID { get; set; }
	}

	public class OtherUserConnectedToGroupArgs {
		/// <summary>
		/// The ID of the group the user connected to
		/// </summary>
		public int GroupID { get; set; }
		/// <summary>
		/// The ID of the user who connected
		/// </summary>
		public int UserID { get; set; }
		/// <summary>
		/// The username of the user who connected
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// The image of the user who connected
		/// </summary>
		public string UserImage { get; set; }
		/// <summary>
		/// The rank of the user who connected
		/// </summary>
		public string UserRank { get; set; }
	}

	public class OtherUserDisconnectedFromGroupArgs {
		/// <summary>
		/// The ID of the group the user disconnected from
		/// </summary>
		public int GroupID { get; set; }
		/// <summary>
		/// The ID of the user who disconnected
		/// </summary>
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
		/// <summary>
		/// The ID of the user who should receive the notification
		/// </summary>
		public int ChatUserID { get; set; }
		/// <summary>
		/// The title of the notification
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// The text of the notification
		/// </summary>
		public string Text { get; set; }
		/// <summary>
		/// The URL to link to when the user views the notification
		/// </summary>
		public string ViewURL { get; set; }
	}

	public class RemoveNotificationArgs {
		/// <summary>
		/// The user to which the notification belongs to
		/// </summary>
		public int ChatUserID { get; set; }
		/// <summary>
		/// The ID of the notification
		/// </summary>
		public int NotificationID { get; set; }
	}

	public class GetNotificationsArgs {
		/// <summary>
		/// The ID of the user whose notifications to get
		/// </summary>
		public int ChatUserID { get; set; }
	}



	public class NewNotificationArgs {
		/// <summary>
		/// The ID of the receiving user
		/// </summary>
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


	public class HasNewMessagesArgs {
		public int ChatUserID { get; set; }
	}

	public class ReceiveNumNewMessagesArgs {
		public int ChatUserID { get; set; }
		public int Num { get; set; }
	}

}

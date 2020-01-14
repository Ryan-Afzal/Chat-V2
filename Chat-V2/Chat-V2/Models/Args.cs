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
		/// The message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Any multimedia information that the message may contain.
		/// </summary>
		public string Multimedia { get; set; }
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

		public AddGroupArgs(int groupId, int membershipId, int numNew, string groupName, string groupImage) {
			GroupID = groupId;
			MembershipID = membershipId;
			NumNew = numNew;
			GroupName = groupName;
			GroupImage = groupImage;
		}

		/// <summary>
		/// The ID of the group
		/// </summary>
		public int GroupID { get; }
		/// <summary>
		/// The ID of the membership belonging to the group
		/// </summary>
		public int MembershipID { get; }
		/// <summary>
		/// Whether the user has unread messages
		/// </summary>
		public int NumNew { get; }
		/// <summary>
		/// The group name
		/// </summary>
		public string GroupName { get; }
		/// <summary>
		/// The group image
		/// </summary>
		public string GroupImage { get; }
	}

	public class RemoveGroupArgs {

		public RemoveGroupArgs(int groupId) {
			GroupID = groupId;
		}

		/// <summary>
		/// The ID of the group to remove
		/// </summary>
		public int GroupID { get; }
	}

	public class OtherUserConnectedToGroupArgs {

		public OtherUserConnectedToGroupArgs(int groupId, int userId, string userName, string userImage, string userRank) {
			GroupID = groupId;
			UserID = userId;
			UserName = userName;
			UserImage = userImage;
			UserRank = userRank;
		}

		/// <summary>
		/// The ID of the group the user connected to
		/// </summary>
		public int GroupID { get; }
		/// <summary>
		/// The ID of the user who connected
		/// </summary>
		public int UserID { get; }
		/// <summary>
		/// The username of the user who connected
		/// </summary>
		public string UserName { get; }
		/// <summary>
		/// The image of the user who connected
		/// </summary>
		public string UserImage { get; }
		/// <summary>
		/// The rank of the user who connected
		/// </summary>
		public string UserRank { get; }
	}

	public class OtherUserConnectedToPersonalGroupArgs {
		public OtherUserConnectedToPersonalGroupArgs(int groupId, int userId) {
			GroupID = groupId;
			UserID = userId;
		}

		/// <summary>
		/// The ID of the group the user connected to
		/// </summary>
		public int GroupID { get; }
		/// <summary>
		/// The ID of the user who connected
		/// </summary>
		public int UserID { get; }
	}

	public class OtherUserDisconnectedFromGroupArgs {

		public OtherUserDisconnectedFromGroupArgs(int groupId, int userId) {
			GroupID = groupId;
			UserID = userId;
		}

		/// <summary>
		/// The ID of the group the user disconnected from
		/// </summary>
		public int GroupID { get; }
		/// <summary>
		/// The ID of the user who disconnected
		/// </summary>
		public int UserID { get; }
	}

	public class OtherUserDisconnectedFromPersonalGroupArgs {

		public OtherUserDisconnectedFromPersonalGroupArgs(int groupId, int userId) {
			GroupID = groupId;
			UserID = userId;
		}

		/// <summary>
		/// The ID of the group the user disconnected from
		/// </summary>
		public int GroupID { get; }
		/// <summary>
		/// The ID of the user who disconnected
		/// </summary>
		public int UserID { get; }
	}

	public class OtherUserActiveInGroupArgs {

		public OtherUserActiveInGroupArgs(int groupId, int userId) {
			GroupID = groupId;
			UserID = userId;
		}

		public int GroupID { get; }
		public int UserID { get; }
	}

	public class OtherUserActiveInPersonalGroupArgs {

		public OtherUserActiveInPersonalGroupArgs(int groupId, int userId) {
			GroupID = groupId;
			UserID = userId;
		}

		public int GroupID { get; }
		public int UserID { get; }
	}

	public class OtherUserInactiveInGroupArgs {

		public OtherUserInactiveInGroupArgs(int groupId, int userId) {
			GroupID = groupId;
			UserID = userId;
		}

		public int GroupID { get; }
		public int UserID { get; }
	}

	public class OtherUserInactiveInPersonalGroupArgs {

		public OtherUserInactiveInPersonalGroupArgs(int groupId, int userId) {
			GroupID = groupId;
			UserID = userId;
		}

		public int GroupID { get; }
		public int UserID { get; }
	}

	public class OtherUserTypingArgs {

		public OtherUserTypingArgs(int groupId, int userId, string userImage) {
			GroupID = groupId;
			UserID = userId;
			UserImage = userImage;
		}

		public int UserID { get; }
		public int GroupID { get; }
		public string UserImage { get; }
	}

	public class OtherUserNotTypingArgs {

		public OtherUserNotTypingArgs(int groupId, int userId) {
			GroupID = groupId;
			UserID = userId;
		}

		public int UserID { get; }
		public int GroupID { get; }
	}

	public class OtherUserAddedToGroupArgs {

		public OtherUserAddedToGroupArgs(int groupId) {
			GroupID = groupId;
		}

		public int GroupID { get; }
	}

	public class OtherUserRemovedFromGroupArgs {

		public OtherUserRemovedFromGroupArgs(int groupId, int userId) {
			GroupID = groupId;
			UserID = userId;
		}

		public int UserID { get; }
		public int GroupID { get; }
	}

	public class ReceiveGroupDataArgs {

		public ReceiveGroupDataArgs(int groupId, string groupName, int numUsers) {
			GroupID = groupId;
			GroupName = groupName;
			NumUsers = numUsers;
		}

		public int GroupID { get; }
		public string GroupName { get; }
		public int NumUsers { get; }
	}

	public class ReceivePersonalGroupDataArgs {
		public ReceivePersonalGroupDataArgs(int groupId, int userId, string userName, string userImage) {
			GroupID = groupId;
			UserID = userId;
			UserName = userName;
			UserImage = userImage;
		}

		/// <summary>
		/// The ID of the group the user connected to
		/// </summary>
		public int GroupID { get; }
		/// <summary>
		/// The ID of the user who connected
		/// </summary>
		public int UserID { get; }
		/// <summary>
		/// The username of the user who connected
		/// </summary>
		public string UserName { get; }
		/// <summary>
		/// The image of the user who connected
		/// </summary>
		public string UserImage { get; }
	}

	public class ReceiveMessageArgs {

		public ReceiveMessageArgs(int senderId, int groupId, string userName, string userImage, string timestamp, string message, string multimedia) {
			SenderID = senderId;
			GroupID = groupId;
			UserName = userName;
			UserImage = userImage;
			Timestamp = timestamp;
			Message = message;
			Multimedia = multimedia;
		}

		public int SenderID { get; }
        public int GroupID { get; }
		public string UserName { get; }
		public string UserImage { get; }
		public string Timestamp { get; }
		public string Message { get; }
		public string Multimedia { get; }
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

		public ReceiveNotificationArgs(Notification notif) {
			ChatUserID = notif.ChatUserID;
			NotificationID = notif.NotificationID;
			Date = notif.Date.ToString();
			Title = notif.Title;
			Text = notif.Text;
			ViewURL = notif.ViewURL;
		}

		public ReceiveNotificationArgs(int chatUserID, int notificationID, string date, string title, string text, string viewURL) {
			ChatUserID = chatUserID;
			NotificationID = notificationID;
			Date = date;
			Title = title;
			Text = text;
			ViewURL = viewURL;
		}

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

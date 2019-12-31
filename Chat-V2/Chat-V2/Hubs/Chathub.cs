using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chat_V2.Hubs {
	public class ChatHub : Hub {

		public UserManager<ChatUser> UserManager { get; }
		public ChatContext ChatContext { get; }

		public ChatHub(UserManager<ChatUser> userManager, ChatContext context) {
			UserManager = userManager;
			ChatContext = context;
		}

		/// <summary>
		/// Called when the client begins the SignalR connection.
		/// 
		/// It increases the count of online threads for a user.
		/// </summary>
		/// <returns></returns>
		public override async Task OnConnectedAsync() {
			await base.OnConnectedAsync();

			var user = await UserManager.GetUserAsync(Context.User);
			user.NumOnline++;
			await ChatContext.SaveChangesAsync();
		}

		/// <summary>
		/// Called when the client disconnects from the SignalR connection.
		/// 
		/// It decreases the count of online threads for a user, and sends the OtherUserDisconnected event to the users in other groups.
		/// </summary>
		/// <param name="exception"></param>
		/// <returns></returns>
		public override async Task OnDisconnectedAsync(Exception exception) {
			await base.OnDisconnectedAsync(exception);

			var userId = int.Parse(Context.UserIdentifier);
			var chatUser = await ChatContext.Users
				.Include(u => u.Memberships)
				.FirstOrDefaultAsync(u => u.Id == userId);
			chatUser.NumOnline--;
			
			if (!chatUser.IsOnline) {
				foreach (Membership membership in chatUser.Memberships) {
					if (membership.IsOnlineInGroup) {
						membership.IsOnlineInGroup = false;
						await (await GetClientsInGroup(membership.GroupID, PermissionRank.USER))
							.SendAsync(
							"OtherUserDisconnectedFromGroup",
							new OtherUserDisconnectedFromGroupArgs() {
								UserID = membership.ChatUserID,
								GroupID = membership.GroupID
							});
					}
				}
			}

			await ChatContext.SaveChangesAsync();
		}

		/// <summary>
		/// Called when the client connects to the server (after <c>OnConnectedAsync()</c>).
		/// 
		/// Used to pass any data needed for connection.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public async Task Connected(ConnectedArgs args) {
			var chatUser = await ChatContext.Users
				.Include(u => u.Memberships)
					.ThenInclude(m => m.Group)
						.ThenInclude(g => g.ChatMessages)
				.FirstOrDefaultAsync(u => u.Id == args.UserID);
			//await ChatContext.SaveChangesAsync();

			foreach (var m in chatUser.Memberships) {
				bool hasNew = false;

				var lastMessage = m.Group.ChatMessages.LastOrDefault();

				if (lastMessage is null || m.LastViewedMessageID is null || lastMessage.ChatMessageID == m.LastViewedMessageID) {
					hasNew = false;
				} else {
					hasNew = true;
					//hasNew = lastMessage.TimeStamp > (await ChatContext.ChatMessage.FirstOrDefaultAsync(c => c.ChatMessageID == m.LastViewedMessageID)).TimeStamp;
				}

				await Clients.Caller.SendAsync(
					"AddGroup", 
					new AddGroupArgs() {
						GroupID = m.GroupID,
						MembershipID = m.MembershipID,
						HasNew = hasNew,
						GroupName = m.Group.Name,
						GroupImage = FileTools.FileSavePath + "/" + m.Group.GroupImage
					});
			}
		}

		/// <summary>
		/// Called when the client disconnects from the server (before <c>OnDisconnectedAsync()</c>).
		/// 
		/// Used to pass any data needed for normal disconnection.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>

		public async Task Disconnected(DisconnectedArgs args) {
			
		}

		public async Task ConnectedToGroup(ConnectedToGroupArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
					.ThenInclude(g => g.Memberships)
						.ThenInclude(m => m.ChatUser)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			membership.IsOnlineInGroup = true;
			await ChatContext.SaveChangesAsync();

			await GetPreviousMessages(new GetPreviousMessagesArgs() {
				MembershipID = args.MembershipID,
				StartIndex = 0,
				Count = 50
			});

			await Clients.Caller.SendAsync(
					"ReceiveGroupData",
					new ReceiveGroupDataArgs() {
						GroupID = membership.Group.GroupID,
						GroupName = membership.Group.Name,
						NumUsers = membership.Group.Memberships.Count
					});

			foreach (var _m in membership.Group.Memberships) {
				if (_m.IsOnlineInGroup) {
					await Clients.Caller.SendAsync(
						"OtherUserConnectedToGroup", 
						new OtherUserConnectedToGroupArgs() {
							GroupID = _m.GroupID,
							UserID = _m.ChatUserID,
							UserName = _m.ChatUser.UserName,
							UserImage = FileTools.FileSavePath + "/" + _m.ChatUser.ProfileImage,
							UserRank = PermissionRank.GetPermissionRankByOrdinal(_m.Rank).Name
						});
					if (_m.IsActiveInGroup) {
						await Clients.Caller.SendAsync(
							"OtherUserActiveInGroup",
							new OtherUserConnectedToGroupArgs() {
								GroupID = _m.GroupID,
								UserID = _m.ChatUserID,
								UserName = _m.ChatUser.UserName,
								UserImage = FileTools.FileSavePath + "/" + _m.ChatUser.ProfileImage,
								UserRank = PermissionRank.GetPermissionRankByOrdinal(_m.Rank).Name
							});
					} else {
						await Clients.Caller.SendAsync(
							"OtherUserInactiveInGroup",
							new OtherUserConnectedToGroupArgs() {
								GroupID = _m.GroupID,
								UserID = _m.ChatUserID,
								UserName = _m.ChatUser.UserName,
								UserImage = FileTools.FileSavePath + "/" + _m.ChatUser.ProfileImage,
								UserRank = PermissionRank.GetPermissionRankByOrdinal(_m.Rank).Name
							});
					}
				}
			}

			var proxy = await GetClientsInGroup(membership.GroupID, PermissionRank.USER);

			await proxy
				.SendAsync(
				"OtherUserConnectedToGroup",
				new OtherUserConnectedToGroupArgs() {
					GroupID = membership.GroupID,
					UserID = membership.ChatUserID,
					UserName = membership.ChatUser.UserName,
					UserRank = PermissionRank.GetPermissionRankByOrdinal(membership.Rank).Name
				});

			await proxy
				.SendAsync(
				"OtherUserActiveInGroup", 
				new OtherUserActiveInGroupArgs() {
					GroupID = membership.GroupID,
					UserID = membership.ChatUserID
				});
		}

		public async Task DisconnectedFromGroup(DisconnectedFromGroupArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
					.ThenInclude(g => g.Memberships)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			membership.IsOnlineInGroup = false;
			await ChatContext.SaveChangesAsync();

			await (await GetClientsInGroup(membership.GroupID, PermissionRank.USER))
				.SendAsync(
				"OtherUserDisconnectedFromGroup",
				new OtherUserDisconnectedFromGroupArgs() {
					UserID = membership.ChatUserID, 
					GroupID = membership.GroupID
				});
		}

		public async Task ActiveInGroup(ActiveInGroupArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
					.ThenInclude(g => g.Memberships)
						.ThenInclude(m => m.ChatUser)
				.Include(m => m.Group)
					.ThenInclude(g => g.ChatMessages)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			membership.IsActiveInGroup = true;
			membership.LastViewedMessageID = membership.Group.ChatMessages.LastOrDefault()?.ChatMessageID;
			await ChatContext.SaveChangesAsync();

			await (await GetClientsInGroup(membership.GroupID, PermissionRank.USER))
				.SendAsync(
				"OtherUserActiveInGroup",
				new OtherUserActiveInGroupArgs() {
					GroupID = membership.GroupID,
					UserID = membership.ChatUserID
				});
		}

		public async Task InactiveInGroup(InactiveInGroupArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
					.ThenInclude(g => g.Memberships)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			membership.IsActiveInGroup = false;
			await ChatContext.SaveChangesAsync();

			await (await GetClientsInGroup(membership.GroupID, PermissionRank.USER))
				.SendAsync(
				"OtherUserInactiveInGroup",
				new OtherUserInactiveInGroupArgs() {
					UserID = membership.ChatUserID,
					GroupID = membership.GroupID
				});
		}

		//UserAddedToGroup
		//UserRemovedFromGroup

		public async Task UserTyping(UserTypingArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(g => g.Group)
					.ThenInclude(g => g.Memberships)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			await GetClientsInGroup(membership.Group, PermissionRank.USER)
				.SendAsync(
					"OtherUserTyping", 
					new OtherUserTypingArgs() { 
						UserID = membership.ChatUserID,
						GroupID = membership.GroupID,
						UserProfileImage = membership.ChatUser.ProfileImage
					});
		}

		public async Task UserNotTyping(UserNotTypingArgs args) {
			var membership = await ChatContext.Membership
				.Include(g => g.Group)
					.ThenInclude(g => g.Memberships)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			await GetClientsInGroup(membership.Group, PermissionRank.USER)
				.SendAsync(
					"OtherUserNotTyping",
					new OtherUserNotTypingArgs() {
						UserID = membership.ChatUserID,
						GroupID = membership.GroupID
					});
		}

		public async Task GetPreviousMessages(GetPreviousMessagesArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
				.ThenInclude(g => g.ChatMessages)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);

			//basic test code that just gets every single previous message regardless of prevStart and prevEnd.
			LinkedList<ReceiveMessageArgs> list = new LinkedList<ReceiveMessageArgs>();
			int i = 0;
			foreach (var m in membership.Group.ChatMessages.AsQueryable().Reverse().Skip(args.StartIndex)) {
				var user = await UserManager.FindByIdAsync(m.ChatUserID + "");
				var rank = PermissionRank.GetPermissionRankByOrdinal(membership.Rank);
				var messageRank = PermissionRank.GetPermissionRankByOrdinal(m.ChatUserRank);//rank of user who sent the message

				if (i >= args.Count) {
					break;
				}

				list.AddLast(await GetArgsFromChatMessageAsync(m, user, membership));

				i++;
			}

			await Clients.Caller.SendAsync(
				"ReceivePreviousMessages",
				list);
		}

		private async Task<ChatMessage> ProcessMessageAsync(Membership membership, string message) {
			ChatMessage output = new ChatMessage() {
				ChatUserID = membership.ChatUserID,
				GroupID = membership.GroupID,
				TimeStamp = DateTime.Now,
				ChatUserRank = membership.Rank
			};

			StringBuilder builder = new StringBuilder();

			builder.Append(message);

			output.Message = builder.ToString();

			return output;
		}

		private async Task<ReceiveMessageArgs> GetArgsFromChatMessageAsync(ChatMessage message, ChatUser chatUser, Membership membership) {
			ReceiveMessageArgs output = new ReceiveMessageArgs {
				SenderID = message.ChatUserID,
				GroupID = message.GroupID
			};

			bool userDeleted = chatUser is null;

			StringBuilder builder = new StringBuilder();

			builder.Append("<div class=\"message container\">");
				builder.Append("<div class=\"row\">");
					builder.Append("<div class=\"message-image col-auto\">");
						builder.Append("<img src=\"");

						if (userDeleted) {
							builder.Append(FileTools.DefaultImagePath + " / ");
						} else {
							builder.Append(FileTools.FileSavePath + "/");
						}

						builder.Append(chatUser?.ProfileImage ?? "defaultProfileImage.png");
						builder.Append("\" width=\"32\" height=\"32\" class=\"rounded-circle img\" />");
					builder.Append("</div>");

					builder.Append("<div class=\"message-container col\">");
						builder.Append("<div class=\"message-header text-wrap row\">");
							builder.Append("<span class=\"message-username\" style=\"color:#");
							builder.Append(PermissionRank.GetPermissionRankByOrdinal(userDeleted ? membership.Rank : message.ChatUserRank).Color);
							builder.Append(";\">");
									builder.Append(chatUser?.UserName ?? message.ChatUserName);
							builder.Append("</span>");
							builder.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
							builder.Append("<span class=\"message-timestamp text-muted\">");
								builder.Append(message.TimeStamp.ToString());
							builder.Append("</span>");
						builder.Append("</div>");

						builder.Append("<div class=\"row\">");
							builder.Append("<span class=\"message-content\">");
								builder.Append(message.Message);
							builder.Append("</span>");
						builder.Append("</div>");
					builder.Append("</div>");
				builder.Append("</div>");
			builder.Append("</div>");

			output.Message = builder.ToString();

			return output;
		}

		public async Task SendMessage(SendMessageArgs args) {
			//Load data from database based on args
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
					.ThenInclude(g => g.ChatMessages)
				.Include(m => m.Group)
					.ThenInclude(g => g.Memberships)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);
			PermissionRank senderRank = PermissionRank.GetPermissionRankByOrdinal(membership.Rank);
			PermissionRank minRank = PermissionRank.GetPermissionRankByOrdinal(args.MinRank);
			ChatUser sender = membership.ChatUser;
			Group group = membership.Group;

			//Log the message
			ChatMessage chatMessage = await ProcessMessageAsync(membership, args.Message);
			group.ChatMessages.Add(chatMessage);
			await ChatContext.SaveChangesAsync();

			//Distribute the message
			List<string> list = new List<string>();

			foreach (var m in group.Memberships) {
				if (m.Rank >= minRank.Ordinal) {
					if (m.IsActiveInGroup) {
						m.LastViewedMessageID = chatMessage.ChatMessageID;
					}

					list.Add(m.ChatUserID + "");
				}
			}

			await ChatContext.SaveChangesAsync();

			await Clients.Users(list)
				.SendAsync(
					"ReceiveMessage",
					await GetArgsFromChatMessageAsync(chatMessage, sender, membership));
		}

		private async Task<Group> GetGroupById(int groupId, bool loadMembers, bool loadMessages, bool tracking) {
			var query = ChatContext.Group;
			if (loadMembers) {
				query.Include(g => g.Memberships);
			}

			if (loadMessages) {
				query.Include(g => g.ChatMessages);
			}

			if (!tracking) {
				query.AsNoTracking();
			}

			Group group = await query.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				throw new ArgumentException("Invalid Group ID");
			}

			return group;
		}

		private async Task<IReadOnlyList<string>> GetUsersInGroup(int groupId, PermissionRank minRank) {
			return GetUsersInGroup(await GetGroupById(groupId, true, false, false), minRank);
		}

		private IReadOnlyList<string> GetUsersInGroup(Group group, PermissionRank minRank) {
			List<string> list = new List<string>();

			foreach (var m in group.Memberships) {
				if (m.Rank >= minRank.Ordinal) {
					list.Add(m.ChatUserID + "");
				}
			}

			return list.AsReadOnly();
		}

		private IClientProxy GetClientsInGroup(Group group, PermissionRank minRank) {
			return Clients.Users(GetUsersInGroup(group, minRank));
		}
		
		private async Task<IClientProxy> GetClientsInGroup(int groupId, PermissionRank minRank) {
			return Clients.Users(await GetUsersInGroup(groupId, minRank));
		}

	}
}

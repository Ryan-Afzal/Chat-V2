﻿using Chat_V2.Areas.Identity.Data;
using Chat_V2.Interfaces;
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
		public IFileOperationProvider FileOperationProvider { get; }

		public ChatHub(UserManager<ChatUser> userManager, ChatContext context, IFileOperationProvider fileConfiguration) {
			UserManager = userManager;
			ChatContext = context;
			FileOperationProvider = fileConfiguration;
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
						await (await GetClientsInGroupAsync(membership.GroupID))
							.SendAsync(
							"OtherUserDisconnectedFromGroup",
							new OtherUserDisconnectedFromGroupArgs(membership.GroupID, membership.ChatUserID)
							);
					}
				}
			}

			await ChatContext.SaveChangesAsync();
		}

		/// <summary>
		/// Called when the client connects to the server (after <see cref="OnConnectedAsync()"/>).
		/// 
		/// Used to pass any data needed for connection.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public async Task Connected(ConnectedArgs args) {
			var chatUser = await ChatContext.Users
				.Include(u => u.Memberships)
					.ThenInclude(m => m.Group)
						.ThenInclude(g => g.Memberships)
							.ThenInclude(m => m.ChatUser)
				.FirstOrDefaultAsync(u => u.Id == args.UserID);
				
			if (!ValidateUser(chatUser)) {
				return;
			}

			foreach (var m in chatUser.Memberships) {
				if (m is PersonalGroupMembership p) {
					var otherUser = p.PersonalGroup.GetOtherUser(chatUser);
					await Clients.Caller
						.SendAsync(
						"AddGroup",
						new AddGroupArgs(
							p.GroupID, 
							p.MembershipID, 
							p.NumNew, 
							otherUser.UserName, 
							FileOperationProvider.FileLoadURL + "/" + otherUser.ProfileImage
							)
						);
				} else if (m is MultiuserGroupMembership g) {
					await Clients.Caller
						.SendAsync(
						"AddGroup",
						new AddGroupArgs(
							g.GroupID, 
							g.MembershipID, 
							g.NumNew, 
							g.MultiuserGroup.Name, 
							FileOperationProvider.FileLoadURL + "/" + g.MultiuserGroup.GroupImage)
						);
				} else {
					throw new InvalidOperationException();
				}
			}
		}

		/// <summary>
		/// Called when the client disconnects from the server normally (before <see cref="OnDisconnectedAsync(Exception)"/>).
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
				
			if (!ValidateUser(membership.ChatUser)) {
				return;
			}

			membership.IsOnlineInGroup = true;
			await ChatContext.SaveChangesAsync();

			await GetPreviousMessages(new GetPreviousMessagesArgs() {
				MembershipID = args.MembershipID,
				StartIndex = 0,
				Count = 50
			});

			var proxy = await GetClientsInGroupAsync(membership.GroupID);

			if (membership is PersonalGroupMembership p) {
				var otherUser = p.PersonalGroup.GetOtherUser(p.ChatUser);

				await Clients.Caller.SendAsync(
					"ReceivePersonalGroupData",
					new ReceivePersonalGroupDataArgs(
						p.Group.GroupID, 
						otherUser.Id, 
						otherUser.UserName, 
						FileOperationProvider.FileLoadURL + "/" + otherUser.ProfileImage)
					);

				foreach (var _m in p.PersonalGroup.PersonalGroupMemberships) {
					if (_m.IsOnlineInGroup) {
						await Clients.Caller.SendAsync(
							"OtherUserConnectedToPersonalGroup",
							new OtherUserConnectedToPersonalGroupArgs(
								_m.GroupID,
								_m.ChatUserID
								)
							);
						if (_m.IsActiveInGroup) {
							await Clients.Caller.SendAsync(
								"OtherUserActiveInPersonalGroup",
								new OtherUserActiveInPersonalGroupArgs(
									_m.GroupID, 
									_m.ChatUserID
									)
								);
						} else {
							await Clients.Caller.SendAsync(
								"OtherUserInactiveInPersonalGroup",
								new OtherUserInactiveInPersonalGroupArgs(
									_m.GroupID, 
									_m.ChatUserID
									)
								);
						}
					}
				}

				await proxy
					.SendAsync(
					"OtherUserConnectedToPersonalGroup",
					new OtherUserConnectedToPersonalGroupArgs(
						p.GroupID,
						p.ChatUserID
						)
					);

				await proxy
					.SendAsync(
					"OtherUserActiveInPersonalGroup",
					new OtherUserActiveInPersonalGroupArgs(membership.GroupID, membership.ChatUserID)
					);
			} else if (membership is MultiuserGroupMembership m) {
				await Clients.Caller.SendAsync(
					"ReceiveGroupData",
					new ReceiveGroupDataArgs(m.Group.GroupID, m.MultiuserGroup.Name, m.Group.Memberships.Count())
					);

				foreach (var _m in m.MultiuserGroup.Memberships.OfType<MultiuserGroupMembership>()) {
					if (_m.IsOnlineInGroup) {
						await Clients.Caller.SendAsync(
							"OtherUserConnectedToGroup",
							new OtherUserConnectedToGroupArgs(
								_m.GroupID,
								_m.ChatUserID,
								_m.ChatUser.UserName,
								FileOperationProvider.FileLoadURL + "/" + _m.ChatUser.ProfileImage,
								PermissionRank.GetPermissionRankByOrdinal(_m.Rank).Name)
							);
						if (_m.IsActiveInGroup) {
							await Clients.Caller.SendAsync(
								"OtherUserActiveInGroup",
								new OtherUserActiveInGroupArgs(_m.GroupID, _m.ChatUserID)
								);
						} else {
							await Clients.Caller.SendAsync(
								"OtherUserInactiveInGroup",
								new OtherUserInactiveInGroupArgs(_m.GroupID, _m.ChatUserID)
								);
						}
					}
				}

				await proxy
					.SendAsync(
					"OtherUserConnectedToGroup",
					new OtherUserConnectedToGroupArgs(
						m.GroupID,
						m.ChatUserID,
						m.ChatUser.UserName,
						FileOperationProvider.FileLoadURL + "/" + m.ChatUser.ProfileImage,
						PermissionRank.GetPermissionRankByOrdinal(m.Rank).Name)
					);

				await proxy
					.SendAsync(
					"OtherUserActiveInGroup",
					new OtherUserActiveInGroupArgs(membership.GroupID, membership.ChatUserID)
					);
			} else {
				throw new InvalidOperationException();
			}
		}

		public async Task DisconnectedFromGroup(DisconnectedFromGroupArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
					.ThenInclude(g => g.Memberships)
						.ThenInclude(m => m.ChatUser)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);
				
			if (!ValidateUser(membership.ChatUser)) {
				return;
			}

			membership.IsOnlineInGroup = false;
			await ChatContext.SaveChangesAsync();

			if (membership is PersonalGroupMembership) {
				await (await GetClientsInGroupAsync(membership.GroupID))
					.SendAsync(
					"OtherUserDisconnectedFromPersonalGroup",
					new OtherUserDisconnectedFromPersonalGroupArgs(membership.GroupID, membership.ChatUserID)
					);
			} else if (membership is MultiuserGroupMembership) {
				await (await GetClientsInGroupAsync(membership.GroupID))
					.SendAsync(
					"OtherUserDisconnectedFromGroup",
					new OtherUserDisconnectedFromGroupArgs(membership.GroupID, membership.ChatUserID)
					);
			} else {
				throw new InvalidOperationException();
			}
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
				
			if (!ValidateUser(membership.ChatUser)) {
				return;
			}

			membership.IsActiveInGroup = true;
			membership.LastViewedMessageID = membership.Group.ChatMessages.LastOrDefault()?.ChatMessageID;
			membership.NumNew = 0;
			await ChatContext.SaveChangesAsync();

			if (membership is PersonalGroupMembership) {
				await (await GetClientsInGroupAsync(membership.GroupID))
					.SendAsync(
					"OtherUserActiveInPersonalGroup",
					new OtherUserActiveInPersonalGroupArgs(membership.GroupID, membership.ChatUserID)
					);
			} else if (membership is MultiuserGroupMembership) {
				await (await GetClientsInGroupAsync(membership.GroupID))
					.SendAsync(
					"OtherUserActiveInGroup",
					new OtherUserActiveInGroupArgs(membership.GroupID, membership.ChatUserID)
					);
			} else {
				throw new InvalidOperationException();
			}
		}

		public async Task InactiveInGroup(InactiveInGroupArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
					.ThenInclude(g => g.Memberships)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);
				
			if (!ValidateUser(membership.ChatUser)) {
				return;
			}

			membership.IsActiveInGroup = false;
			await ChatContext.SaveChangesAsync();

			if (membership is PersonalGroupMembership) {
				await (await GetClientsInGroupAsync(membership.GroupID))
					.SendAsync(
					"OtherUserInactiveInPersonalGroup",
					new OtherUserInactiveInPersonalGroupArgs(membership.GroupID, membership.ChatUserID)
					);
			} else if (membership is MultiuserGroupMembership) {
				await (await GetClientsInGroupAsync(membership.GroupID))
					.SendAsync(
					"OtherUserInactiveInGroup",
					new OtherUserInactiveInGroupArgs(membership.GroupID, membership.ChatUserID)
					);
			} else {
				throw new InvalidOperationException();
			}
		}

		public async Task UserTyping(UserTypingArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(g => g.Group)
					.ThenInclude(g => g.Memberships)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);
				
			if (!ValidateUser(membership.ChatUser)) {
				return;
			}

			await GetClientsInGroup(membership.Group)
				.SendAsync(
					"OtherUserTyping", 
					new OtherUserTypingArgs(membership.GroupID, membership.ChatUserID, membership.ChatUser.ProfileImage)
					);
		}

		public async Task UserNotTyping(UserNotTypingArgs args) {
			var membership = await ChatContext.Membership
				.Include(g => g.Group)
					.ThenInclude(g => g.Memberships)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);
				
			if (!ValidateUser(membership.ChatUser)) {
				return;
			}

			await GetClientsInGroup(membership.Group)
				.SendAsync(
					"OtherUserNotTyping",
					new OtherUserNotTypingArgs( membership.GroupID, membership.ChatUserID)
					);
		}

		public async Task GetPreviousMessages(GetPreviousMessagesArgs args) {
			var membership = await ChatContext.Membership
				.Include(m => m.ChatUser)
				.Include(m => m.Group)
					.ThenInclude(g => g.ChatMessages)
				.FirstOrDefaultAsync(m => m.MembershipID == args.MembershipID);
				
			if (!ValidateUser(membership.ChatUser)) {
				return;
			}

			LinkedList<ReceiveMessageArgs> list = new LinkedList<ReceiveMessageArgs>();
			int i = 0;
			foreach (var m in membership.Group.ChatMessages.AsQueryable().Reverse().Skip(args.StartIndex)) {
				var user = await UserManager.FindByIdAsync(m.ChatUserID + "");

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

		private string FormatDate(DateTime date) {
			return date.ToString("yyyy-MM-dd HH:mm:ss");
		}

		/// <summary>
		/// Creates a <c>ChatMessage</c> out of a membership and a message.
		/// </summary>
		/// <param name="membership"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		private async Task<ChatMessage> ProcessMessageAsync(Membership membership, string message, string multimedia) {
			ChatMessage output = new ChatMessage() {
				ChatUserID = membership.ChatUserID,
				GroupID = membership.GroupID,
				Timestamp = DateTime.Now,
				Message = message,
				Multimedia = multimedia
			};

			return output;
		}

		private async Task<ReceiveMessageArgs> GetArgsFromChatMessageAsync(ChatMessage message, ChatUser chatUser, Membership membership) {
			ReceiveMessageArgs output = new ReceiveMessageArgs(
				message.ChatUserID, 
				message.GroupID,
				chatUser.UserName,
				FileOperationProvider.FileLoadURL + "/" + chatUser.ProfileImage,
				FormatDate(message.Timestamp),
				message.Message,
				message.Multimedia
				);

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
				
			if (!ValidateUser(membership.ChatUser)) {
				return;
			}
			
			ChatUser sender = membership.ChatUser;
			Group group = membership.Group;

			//Log the message
			ChatMessage chatMessage = await ProcessMessageAsync(membership, args.Message, args.Multimedia);
			group.ChatMessages.Add(chatMessage);
			await ChatContext.SaveChangesAsync();

			//Distribute the message
			List<string> list = new List<string>();

			foreach (var m in group.Memberships) {
				if (m.IsOnlineInGroup) {
					if (m.IsActiveInGroup) {
						m.LastViewedMessageID = chatMessage.ChatMessageID;
					}

					list.Add(m.ChatUserID + "");
				} else {
					m.NumNew++;
				}
			}

			await ChatContext.SaveChangesAsync();

			await Clients.Users(list)
				.SendAsync(
					"ReceiveMessage",
					await GetArgsFromChatMessageAsync(chatMessage, sender, membership));
		}

		private async Task<Group> GetGroupByIdAsync(int groupId, bool loadMembers, bool loadMessages, bool tracking) {
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

		private async Task<IReadOnlyList<string>> GetUsersInGroupAsync(int groupId) {
			return GetUsersInGroup(await GetGroupByIdAsync(groupId, true, false, false));
		}

		private IReadOnlyList<string> GetUsersInGroup(Group group) {
			List<string> list = new List<string>();

			foreach (var m in group.Memberships) {
				list.Add(m.ChatUserID + "");
			}

			return list.AsReadOnly();
		}

		private IClientProxy GetClientsInGroup(Group group) {
			return Clients.Users(GetUsersInGroup(group));
		}
		
		private async Task<IClientProxy> GetClientsInGroupAsync(int groupId) {
			return Clients.Users(await GetUsersInGroupAsync(groupId));
		}

		private bool ValidateUser(ChatUser chatUser) {
			return Context.UserIdentifier.Equals(chatUser.Id + "");
		}

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Hubs;
using Chat_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat_V2.Pages {
	[Authorize]
	public class ProfileModel : PageModel {

		public class JoinInvitationInputModel {
			public int ChatUserID { get; set; }
			public int InvitationID { get; set; }
		}

		public class SendChatInvitationInputModel {
			public int ChatUserID { get; set; }
			public int SenderID { get; set; }
			public string Message { get; set; }
		}

		public class PersonalChatInvitationInputModel {
			public int ChatUserID { get; set; }
			public int InvitationID { get; set; }
		}

		public class DismissNotificationInputModel {
			public int ChatUserID { get; set; }
			public int NotificationID { get; set; }
		}

		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<ProfileModel> _logger;
		private readonly IHubContext<NotifHub> _hubContext;

		public ProfileModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<ProfileModel> logger, IHubContext<NotifHub> hubContext) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
			_hubContext = hubContext;
		}

		[BindProperty]
		public ChatUser ChatUser { get; set; }
		[BindProperty]
		public bool IsThisUser { get; set; }
		[BindProperty]
		public bool HasChat { get; set; }
		[BindProperty]
		public bool InviteSent { get; set; }

		[BindProperty]
		public JoinInvitationInputModel JoinInvitationInput { get; set; }

		[BindProperty]
		public SendChatInvitationInputModel SendChatInvitationInput { get; set; }

		[BindProperty]
		public PersonalChatInvitationInputModel PersonalChatInvitationInput { get; set; }

		[BindProperty]
		public DismissNotificationInputModel DismissNotificationInput { get; set; }

		public async Task<IActionResult> OnGetAsync(int? userId) {
			if (userId == null) {
				return BadRequest();
			}

			var currentUserId = (await _userManager.GetUserAsync(User)).Id;

			IsThisUser = currentUserId == userId.Value;

			IQueryable<ChatUser> query = _context.Users;

			if (IsThisUser) {
				HasChat = false;
				query = query
					.Include(u => u.Memberships)
					.Include(u => u.GroupJoinInvitations)
						.ThenInclude(i => i.Group)
					.Include(u => u.PersonalChatInvitations);
			}

			var user = await query.FirstOrDefaultAsync(u => u.Id == userId.Value);

			if (user == null) {
				return NotFound();
			}

			if (!IsThisUser) {
				HasChat = await _context.Membership
					.OfType<PersonalGroupMembership>()
					.Include(m => m.Group)
						.ThenInclude(g => g.Memberships)
					.Where(m => m.ChatUserID == currentUserId)
					.AnyAsync(m => m.Group.Memberships.FirstOrDefault(_m => _m.ChatUserID != currentUserId).ChatUserID == user.Id);

				if (!HasChat) {
					InviteSent = await _context.Users
						.Include(u => u.PersonalChatInvitations)
						.AnyAsync(u => u.PersonalChatInvitations.Any(i => i.ChatUserID == user.Id));
				}
			}

			ChatUser = user;
			JoinInvitationInput = new JoinInvitationInputModel();
			SendChatInvitationInput = new SendChatInvitationInputModel();
			PersonalChatInvitationInput = new PersonalChatInvitationInputModel();
			DismissNotificationInput = new DismissNotificationInputModel();

			return Page();
		}

		public async Task<IActionResult> OnPostAcceptJoinInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var chatUser = await _context.Users
				.Include(u => u.GroupJoinInvitations)
				.FirstOrDefaultAsync(g => g.Id == JoinInvitationInput.ChatUserID);

			if (chatUser == null) {
				return NotFound();
			}

			var invitation = chatUser.GroupJoinInvitations
				.FirstOrDefault(i => i.GroupJoinInvitationID == JoinInvitationInput.InvitationID);

			if (invitation == null) {
				return NotFound();
			}

			var membership = new MultiuserGroupMembership() {
				ChatUserID = invitation.ChatUserID,
				GroupID = invitation.GroupID,
				IsActiveInGroup = false,
				IsOnlineInGroup = false,
				Rank = PermissionRank.USER.Ordinal,
				LastViewedMessageID = null,
				NumNew = 0
			};

			await _context.Membership.AddAsync(membership);
			chatUser.GroupJoinInvitations.Remove(invitation);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostRejectJoinInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var chatUser = await _context.Users
				.Include(u => u.GroupJoinInvitations)
				.FirstOrDefaultAsync(g => g.Id == JoinInvitationInput.ChatUserID);

			if (chatUser == null) {
				return NotFound();
			}

			var invitation = chatUser.GroupJoinInvitations
				.FirstOrDefault(i => i.GroupJoinInvitationID == JoinInvitationInput.InvitationID);

			if (invitation == null) {
				return NotFound();
			}

			chatUser.GroupJoinInvitations.Remove(invitation);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostAcceptPersonalChatInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var chatUser = await _context.Users
				.Include(u => u.PersonalChatInvitations)
				.FirstOrDefaultAsync(g => g.Id == PersonalChatInvitationInput.ChatUserID);

			if (chatUser == null) {
				return NotFound();
			}

			var invitation = chatUser.PersonalChatInvitations
				.FirstOrDefault(i => i.PersonalChatInvitationID == PersonalChatInvitationInput.InvitationID);

			if (invitation == null) {
				return NotFound();
			}

			var group = new PersonalGroup() {
				DateCreated = DateTime.Now
			};

			await _context.Group.AddAsync(group);
			await _context.SaveChangesAsync();

			var membership1 = new PersonalGroupMembership() {
				ChatUserID = invitation.ChatUserID,
				GroupID = group.GroupID,
				IsActiveInGroup = false,
				IsOnlineInGroup = false,
				LastViewedMessageID = null,
				NumNew = 0
			};

			var membership2 = new PersonalGroupMembership() {
				ChatUserID = invitation.SenderID,
				GroupID = group.GroupID,
				IsActiveInGroup = false,
				IsOnlineInGroup = false,
				LastViewedMessageID = null,
				NumNew = 0
			};

			await _context.Membership.AddAsync(membership1);
			await _context.Membership.AddAsync(membership2);
			chatUser.PersonalChatInvitations.Remove(invitation);
			await _context.SaveChangesAsync();

			Notification notif = new Notification() {
				ChatUserID = invitation.SenderID,
				Date = DateTime.Now,
				Title = $"{chatUser.UserName} accepted your invite",
				Text = $"{chatUser.UserName} has accepted your chat invitation.",
				ViewURL = "/Chat"
			};

			(await _context.Users.FirstOrDefaultAsync(u => u.Id == invitation.SenderID)).Notifications.Add(notif);
			await _context.SaveChangesAsync();

			IClientProxy proxy = _hubContext.Clients.User(invitation.SenderID + "");

			await proxy.SendAsync("NewNotification",
				new NewNotificationArgs() {
					ChatUserID = notif.ChatUserID
				});

			await proxy.SendAsync("ReceiveNotification",
				new ReceiveNotificationArgs() {
					ChatUserID = notif.ChatUserID,
					NotificationID = notif.NotificationID,
					Date = notif.Date.ToString(),
					Title = notif.Title,
					Text = notif.Text,
					ViewURL = notif.ViewURL
				});

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostRejectPersonalChatInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var chatUser = await _context.Users
				.Include(u => u.PersonalChatInvitations)
				.FirstOrDefaultAsync(g => g.Id == PersonalChatInvitationInput.ChatUserID);

			if (chatUser == null) {
				return NotFound();
			}

			var invitation = chatUser.PersonalChatInvitations
				.FirstOrDefault(i => i.PersonalChatInvitationID == PersonalChatInvitationInput.InvitationID);

			if (invitation == null) {
				return NotFound();
			}

			chatUser.PersonalChatInvitations.Remove(invitation);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostSendChatInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			var currentUser = await _userManager.GetUserAsync(User);
			var user = await _context.Users
				.Include(u => u.PersonalChatInvitations)
				.FirstOrDefaultAsync(u => u.Id == SendChatInvitationInput.ChatUserID);

			var invitation = new PersonalChatInvitation() {
				ChatUserID = user.Id,
				SenderID = currentUser.Id,
				DateSent = DateTime.Now,
				Message = SendChatInvitationInput.Message
			};

			user.PersonalChatInvitations.Add(invitation);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}
	}
}
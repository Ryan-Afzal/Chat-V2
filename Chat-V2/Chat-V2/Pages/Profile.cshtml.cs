using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Extensions;
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

		public ChatUser ChatUser { get; set; }
		public bool IsThisUser { get; set; }
		public bool HasChat { get; set; }
		public bool InviteSent { get; set; }
		public bool OtherUserSentInvite { get; set; }

		[BindProperty]
		public JoinInvitationInputModel JoinInvitationInput { get; set; }

		[BindProperty]
		public SendChatInvitationInputModel SendChatInvitationInput { get; set; }

		[BindProperty]
		public PersonalChatInvitationInputModel PersonalChatInvitationInput { get; set; }

		[BindProperty]
		public DismissNotificationInputModel DismissNotificationInput { get; set; }

		public async Task<IActionResult> OnGetAsync(int? userId) {
			if (userId is null) {
				return BadRequest();
			}

			var currentUserId = (await _userManager.GetUserAsync(User)).Id;

			IsThisUser = currentUserId == userId.Value;

			IQueryable<ChatUser> query = _context.Users;

			if (IsThisUser) {
				HasChat = false;
				InviteSent = false;
				OtherUserSentInvite = false;

				query = query
					.Include(u => u.Memberships)
					.Include(u => u.GroupJoinInvitations)
						.ThenInclude(i => i.Group)
					.Include(u => u.PersonalChatInvitations);
			}

			var chatUser = await query.FirstOrDefaultAsync(u => u.Id == userId.Value);

			if (chatUser is null) {
				return NotFound();
			}

			if (!IsThisUser) {
				HasChat = await _context.Membership
					.OfType<PersonalGroupMembership>()
					.Include(m => m.Group)
						.ThenInclude(g => g.Memberships)
					.Where(m => m.ChatUserID == currentUserId)
					.AnyAsync(m => m.Group.Memberships.FirstOrDefault(_m => _m.ChatUserID != currentUserId).ChatUserID == chatUser.Id);

				if (!HasChat) {
					InviteSent = await _context.PersonalChatInvitation
						.AnyAsync(i => i.ChatUserID == chatUser.Id && i.SenderID == currentUserId);

					if (InviteSent) {
						OtherUserSentInvite = false;
					} else {
						OtherUserSentInvite = await _context.PersonalChatInvitation
							.AnyAsync(i => i.ChatUserID == currentUserId && i.SenderID == chatUser.Id);
					}
				}
			}

			ChatUser = chatUser;
			JoinInvitationInput = new JoinInvitationInputModel();
			SendChatInvitationInput = new SendChatInvitationInputModel();
			PersonalChatInvitationInput = new PersonalChatInvitationInputModel();
			DismissNotificationInput = new DismissNotificationInputModel();

			return Page();
		}

		public async Task<IActionResult> OnPostAcceptJoinInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var currentUser = await _userManager.GetUserAsync(User);
			var chatUser = await _context.Users
				.FirstOrDefaultAsync(g => g.Id == JoinInvitationInput.ChatUserID);

			if (chatUser is null) {
				return BadRequest();
			}

			if (currentUser.Id != chatUser.Id) {
				return BadRequest();
			}

			var invitation = await _context.GroupJoinInvitation
				.FirstOrDefaultAsync(i => i.GroupJoinInvitationID == JoinInvitationInput.InvitationID);

			if (invitation is null) {
				return BadRequest();
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
			_context.GroupJoinInvitation.Remove(invitation);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostRejectJoinInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var currentUser = await _userManager.GetUserAsync(User);
			var chatUser = await _context.Users
				.FirstOrDefaultAsync(g => g.Id == JoinInvitationInput.ChatUserID);

			if (chatUser is null) {
				return BadRequest();
			}

			if (currentUser.Id != chatUser.Id) {
				return BadRequest();
			}

			var invitation = await _context.GroupJoinInvitation
				.FirstOrDefaultAsync(i => i.GroupJoinInvitationID == JoinInvitationInput.InvitationID);

			if (invitation is null) {
				return BadRequest();
			}

			_context.GroupJoinInvitation.Remove(invitation);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostAcceptPersonalChatInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var currentUser = await _userManager.GetUserAsync(User);
			var chatUser = await _context.Users
				.FirstOrDefaultAsync(g => g.Id == PersonalChatInvitationInput.ChatUserID);

			if (chatUser is null) {
				return BadRequest();
			}

			if (currentUser.Id != chatUser.Id) {
				return BadRequest();
			}

			var invitation = await _context.PersonalChatInvitation
				.FirstOrDefaultAsync(i => i.PersonalChatInvitationID == PersonalChatInvitationInput.InvitationID);

			if (invitation is null) {
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
			_context.PersonalChatInvitation.Remove(invitation);

			Notification notif = new Notification() {
				ChatUserID = invitation.SenderID,
				Date = DateTime.Now,
				Title = $"{chatUser.UserName} accepted your invite",
				Text = $"{chatUser.UserName} has accepted your chat invitation.",
				ViewURL = "/Chat"
			};

			await _context.Notification.AddAsync(notif);
			await _context.SaveChangesAsync();

			await _hubContext.SendNotificationAsync(notif);

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostRejectPersonalChatInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var currentUser = await _userManager.GetUserAsync(User);
			var chatUser = await _context.Users
				.FirstOrDefaultAsync(g => g.Id == PersonalChatInvitationInput.ChatUserID);

			if (chatUser is null) {
				return BadRequest();
			}

			if (currentUser.Id != chatUser.Id) {
				return BadRequest();
			}

			var invitation = await _context.PersonalChatInvitation
				.FirstOrDefaultAsync(i => i.PersonalChatInvitationID == PersonalChatInvitationInput.InvitationID);

			if (invitation is null) {
				return NotFound();
			}

			_context.PersonalChatInvitation.Remove(invitation);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostSendChatInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			var currentUser = await _userManager.GetUserAsync(User);
			var chatUser = await _context.Users
				.FirstOrDefaultAsync(u => u.Id == SendChatInvitationInput.ChatUserID);

			if (currentUser.Id == chatUser.Id) {
				return BadRequest();
			}

			bool hasChat = await _context.Membership
					.OfType<PersonalGroupMembership>()
					.Include(m => m.Group)
						.ThenInclude(g => g.Memberships)
					.Where(m => m.ChatUserID == currentUser.Id)
					.AnyAsync(m => m.Group.Memberships.FirstOrDefault(_m => _m.ChatUserID != currentUser.Id).ChatUserID == chatUser.Id);

			if (hasChat) {
				return BadRequest();
			}

			bool eitherInviteSent = await _context.PersonalChatInvitation
				.AnyAsync(i => (i.ChatUserID == chatUser.Id && i.SenderID == currentUser.Id)
							|| (i.ChatUserID == currentUser.Id && i.SenderID == chatUser.Id));

			if (eitherInviteSent) {
				return BadRequest();
			}

			var invitation = new PersonalChatInvitation() {
				ChatUserID = chatUser.Id,
				SenderID = currentUser.Id,
				DateSent = DateTime.Now,
				Message = SendChatInvitationInput.Message
			};

			await _context.PersonalChatInvitation.AddAsync(invitation);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}
	}
}

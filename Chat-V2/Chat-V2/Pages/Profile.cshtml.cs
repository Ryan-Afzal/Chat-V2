using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat_V2.Pages {
	[Authorize]
	public class ProfileModel : PageModel {

		public class JoinInvitationInputModel {
			public int ChatUserID { get; set; }
			public int InvitationID { get; set; }
		}

		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<ProfileModel> _logger;

		public ProfileModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<ProfileModel> logger) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
		}

		[BindProperty]
		public ChatUser ChatUser { get; set; }
		[BindProperty]
		public bool IsThisUser { get; set; }

		[BindProperty]
		public JoinInvitationInputModel JoinInvitationInput { get; set; }

		public async Task<IActionResult> OnGetAsync(int? userId) {
			if (userId == null) {
				return BadRequest();
			}

			var user = await _context.Users
				.Include(u => u.Memberships)
				.Include(u => u.GroupJoinInvitations)
					.ThenInclude(i => i.Group)
				.FirstOrDefaultAsync(u => u.Id == userId.Value);

			if (user == null) {
				return NotFound();
			}

			ChatUser = user;
			IsThisUser = (await _userManager.GetUserAsync(User)).Id == user.Id;
			JoinInvitationInput = new JoinInvitationInputModel();

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

			Membership membership = new Membership() {
				ChatUserID = invitation.ChatUserID,
				GroupID = invitation.GroupID,
				IsActiveInGroup = false,
				IsOnlineInGroup = false,
				MembershipStatus = new MembershipStatus() {
					DateIssued = DateTime.Now,
					Expiration = DateTime.Now,
					Type = MembershipStatusType.NONE
				},
				Rank = PermissionRank.USER.Ordinal
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
	}
}
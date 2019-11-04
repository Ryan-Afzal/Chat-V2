using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat_V2.Pages {
	[Authorize]
	public class ConfirmDeleteGroupModel : PageModel {
		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<ConfirmBanUserModel> _logger;

		public ConfirmDeleteGroupModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<ConfirmBanUserModel> logger) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
		}

		[BindProperty]
		public Group Group { get; set; }
		[BindProperty]
		public int MembershipID { get; set; }

		[BindProperty]
		public int MembershipIDInput { get; set; }
		
		public async Task<IActionResult> OnGetAsync(int? groupId, string returnUrl = null) {
			if (groupId == null) {
				return BadRequest();
			}

			var group = await _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				return NotFound();
			}

			var currentUserId = (await _userManager.GetUserAsync(User)).Id;

			var currentMembership = group.Memberships
				.FirstOrDefault(m => m.ChatUserID == currentUserId);

			if (currentMembership == null) {
				return BadRequest();
			}

			if (currentMembership.Rank < PermissionRank.OWNER.Ordinal) {
				return Forbid();
			}

			MembershipID = currentMembership.MembershipID;
			Group = group;

			return Page();
		}

		public async Task<IActionResult> OnPostCancelAsync(int? groupId, string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			if (groupId == null) {
				return BadRequest();
			}

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostConfirmAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			var membership = await _context.Membership
				.Include(m => m.Group)
				.Include(m => m.ChatUser)
				.FirstOrDefaultAsync(m => m.MembershipID == MembershipIDInput);

			if (membership == null) {
				return BadRequest();
			}

			var currentUser = await _userManager.GetUserAsync(User);

			if (membership.ChatUserID != currentUser.Id) {
				return BadRequest();
			}

			if (membership.Rank < PermissionRank.OWNER.Ordinal) {
				return Forbid();
			}

			_context.Group.Remove(membership.Group);

			await _context.SaveChangesAsync();

			return LocalRedirect("/Groups");
		}
	}
}
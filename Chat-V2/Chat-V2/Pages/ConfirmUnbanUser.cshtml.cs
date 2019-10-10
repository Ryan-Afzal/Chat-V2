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
	public class ConfirmUnbanUserModel : PageModel {
		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<ConfirmUnbanUserModel> _logger;

		public ConfirmUnbanUserModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<ConfirmUnbanUserModel> logger) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
		}

		[BindProperty]
		public int UserID { get; set; }
		[BindProperty]
		public string UserName { get; set; }
		[BindProperty]
		public int GroupID { get; set; }
		
		public async Task<IActionResult> OnGetAsync(int? userId, int? groupId) {
			if (userId == null || groupId == null) {
				return BadRequest();
			}

			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null) {
				return NotFound();
			}

			var currentUser = await _userManager.GetUserAsync(User);

			var group = await _context.Groups
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				return NotFound();
			}

			var current = group.Memberships.FirstOrDefault(m => m.ChatUserID == currentUser.Id);

			if (current == null || current.Rank < PermissionRank.OFFICER.Ordinal) {
				return BadRequest();
			}

			UserID = user.Id;
			UserName = user.UserName;
			GroupID = group.GroupID;

			return Page();
		}

		public async Task<IActionResult> OnPostCancelAsync(int? groupId) {
			if (groupId == null) {
				return BadRequest();
			}

			return LocalRedirect("/Group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostConfirmAsync(int? userId, int? groupId) {
			if (groupId == null || userId == null) {
				return BadRequest();
			}

			var group = await _context.Groups
				.Include(g => g.BannedUsers)
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			var currentUser = await _userManager.GetUserAsync(User);
			var currentMembership = group.Memberships.FirstOrDefault(m => m.ChatUserID == currentUser.Id);

			if (currentMembership == null || currentMembership.Rank < PermissionRank.OFFICER.Ordinal) {
				return BadRequest();
			}

			group.BannedUsers.Remove(await _context.Users.FirstOrDefaultAsync(u => u.Id == userId));

			await _context.SaveChangesAsync();

			return LocalRedirect("/Group?groupId=" + groupId);
		}
	}
}
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

		public class JoinGroupInvitationInputModel {
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
		public JoinGroupInvitationInputModel JoinGroupInvitationInput { get; set; }

		public async Task<IActionResult> OnGetAsync(int? userId) {
			if (userId == null) {
				return BadRequest();
			}

			var user = await _context.Users
				.Include(u => u.Memberships)
				.Include(u => u.GroupJoinInvitations)
					.ThenInclude(i => i.Group)
				.Include(u => u.ProfileImage)
				.FirstOrDefaultAsync(u => u.Id == userId.Value);

			if (user == null) {
				return NotFound();
			}

			ChatUser = user;
			IsThisUser = (await _userManager.GetUserAsync(User)).Id == user.Id;
			JoinGroupInvitationInput = new JoinGroupInvitationInputModel();

			return Page();
		}

		public async Task<IActionResult> OnPostAcceptJoinInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			throw new NotImplementedException();
		}

		public async Task<IActionResult> OnPostRejectJoinInvitationAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			throw new NotImplementedException();
		}
	}
}
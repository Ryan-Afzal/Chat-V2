using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat_V2.Areas.Identity.Pages.Account.Manage {
	[Authorize]
	public class DeletePersonalDataModel : PageModel {
		private readonly ChatContext _context;
		private readonly UserManager<ChatUser> _userManager;
		private readonly SignInManager<ChatUser> _signInManager;
		private readonly ILogger<DeletePersonalDataModel> _logger;

		public DeletePersonalDataModel(
			ChatContext context,
			UserManager<ChatUser> userManager,
			SignInManager<ChatUser> signInManager,
			ILogger<DeletePersonalDataModel> logger) {
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel {
			[Required]
			[DataType(DataType.Password)]
			public string Password { get; set; }
		}

		public bool RequirePassword { get; set; }

		public async Task<IActionResult> OnGetAsync() {
			var user = await _userManager.GetUserAsync(User);
			if (user is null) {
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			RequirePassword = await _userManager.HasPasswordAsync(user);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync() {
			var userId = (await _userManager.GetUserAsync(User)).Id;
			var user = await _context.Users
				.Include(u => u.Memberships)
				.Include(u => u.GroupJoinInvitations)
				.Include(u => u.Notifications)
				.FirstOrDefaultAsync(u => u.Id == userId);
			if (user is null) {
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			RequirePassword = await _userManager.HasPasswordAsync(user);
			if (RequirePassword) {
				if (!await _userManager.CheckPasswordAsync(user, Input.Password)) {
					ModelState.AddModelError(string.Empty, "Incorrect password.");
					return Page();
				}
			}

			//var result = await _userManager.DeleteAsync(user);
			//var userId = await _userManager.GetUserIdAsync(user);
			//if (!result.Succeeded) {
			//	throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
			//}

			await user.DeleteAsync(_context);

			await _signInManager.SignOutAsync();

			//_logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

			return Redirect("~/");
		}
	}
}

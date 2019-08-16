using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Chat_V2.Pages {

	public class ChatModel : PageModel {

		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ILogger<ChatModel> _logger;

		public ChatModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ILogger<ChatModel> logger) {
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
		}

		[BindProperty]
		public ChatUser ChatUser { get; private set; }

		public async Task<IActionResult> OnGetAsync() {
			if (_signInManager.IsSignedIn(User)) {
				ChatUser = await _userManager.GetUserAsync(User);
				return Page();
			} else {
				return LocalRedirect("/Identity/Account/Login");
			}
		}

	}

}
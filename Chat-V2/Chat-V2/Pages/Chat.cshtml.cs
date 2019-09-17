using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Chat_V2.Pages {

	public class ChatModel : PageModel {

		public class ChatViewModel {
			public ChatUser ChatUser { get; set; }
		}

		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<ChatModel> _logger;

		public ChatModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<ChatModel> logger) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
		}

		/// <summary>
		/// Initial data goes here. DATA SHOULD NEVER BE SAVED TO THE VIEWMODEL!!!!!!!!!!
		/// </summary>
		[BindProperty]
		public ChatViewModel ViewModel { get; private set; }

		public async Task<IActionResult> OnGetAsync() {
			if (_signInManager.IsSignedIn(User)) {
                var chatUser = await _userManager.GetUserAsync(User);

				ViewModel = new ChatViewModel() {
					ChatUser = chatUser
				};

				return Page();
			} else {
				return LocalRedirect("/Identity/Account/Login");
			}
		}

	}

}

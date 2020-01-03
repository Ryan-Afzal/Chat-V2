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
using Newtonsoft.Json;

namespace Chat_V2.Pages {
	[Authorize]
	public class ChatModel : PageModel {

		public class ChatViewModel {
			public ChatUser ChatUser { get; set; }
		}

		public class SearchGroupInputModel {
			public string NameQuery { get; set; }
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

		[BindProperty]
		public ChatViewModel ViewModel { get; private set; }

		[BindProperty]
		public SearchGroupInputModel SearchGroupInput { get; set; }

		public async Task<IActionResult> OnGetAsync() {
			var chatUser = await _userManager.GetUserAsync(User);

			if (chatUser is null) {
				return BadRequest();
			}

			ViewModel = new ChatViewModel() {
				ChatUser = chatUser
			};

			return Page();
		}

		public async Task<IActionResult> OnPostSearchGroupsAsync() {
			return LocalRedirect("/Groups?SearchString=" + SearchGroupInput.NameQuery);
		}

		public async Task<IActionResult> OnPostCreateNewGroupAsync() {
			return LocalRedirect("/CreateGroup");
		}
	}

}

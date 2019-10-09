﻿using System;
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

		public class EditInputModel {
			public string ProfileDescription { get; set; }
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
		public EditInputModel Input { get; set; }

		public async Task<IActionResult> OnGetAsync(int? userId) {
			if (userId == null) {
				return BadRequest();
			}

			var user = await _context.Users
				.Include(u => u.Memberships)
				.Include(u => u.GroupJoinInvitations)
				.Include(u => u.Image)
				.FirstOrDefaultAsync(u => u.Id == userId.Value);

			if (user == null) {
				return NotFound();
			}

			ChatUser = user;
			IsThisUser = (await _userManager.GetUserAsync(User)).Id == user.Id;
			Input = new EditInputModel();

			return Page();
		}

		public async Task<IActionResult> OnPostCancelAsync(int? userId) {
			if (userId == null) {
				return BadRequest();
			}

			return LocalRedirect("/Profile?userId=" + userId.Value);
		}

		public async Task<IActionResult> OnPostSaveAsync(int? userId) {
			if (userId == null) {
				return BadRequest();
			}

			var user = await _context.Users
				.Include(u => u.Memberships)
				.Include(u => u.GroupJoinInvitations)
				.Include(u => u.Image)
				.FirstOrDefaultAsync(u => u.Id == userId.Value);

			if (user == null) {
				return NotFound();
			}



			throw new NotImplementedException();
		}
	}
}
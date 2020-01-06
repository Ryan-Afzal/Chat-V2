using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Chat_V2.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Chat_V2.Interfaces;

namespace Chat_V2.Areas.Identity.Pages.Account {
	[AllowAnonymous]
	public class RegisterModel : PageModel {

		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<RegisterModel> _logger;
		private readonly IEmailSender _emailSender;
		private readonly IWebHostEnvironment _env;
		private readonly IFileOperationProvider _fileConfiguration;

		public RegisterModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<RegisterModel> logger, IEmailSender emailSender, IWebHostEnvironment env, IFileOperationProvider fileConfiguration) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
			_emailSender = emailSender;
			_env = env;
			_fileConfiguration = fileConfiguration;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public string ReturnUrl { get; set; }

		public class InputModel {

			[Required]
			[Display(Name = "Username")]
			public string Username { get; set; }

			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; }

			[Display(Name = "First Name")]
			public string FirstName { get; set; }

			[Display(Name = "Last Name")]
			public string LastName { get; set; }

			[Required]
			[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }

		}

		public void OnGet(string returnUrl = null) {
			ReturnUrl = returnUrl;
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			if (ModelState.IsValid) {
				var user = new ChatUser() {
					UserName = Input.Username,
					Email = Input.Email,
					FirstName = Input.FirstName,
					LastName = Input.LastName,
					ProfileDescription = "",
					ProfileImage = _fileConfiguration.SaveFileFromDefault(_fileConfiguration.DefaultUserProfileImage),
					IsEnabled = true,
					NumOnline = 0,
				};

				var result = await _userManager.CreateAsync(user, Input.Password);
				if (result.Succeeded) {
					Group group = await _context.Group.FirstAsync();
					Membership membership = new Membership() {
						GroupID = group.GroupID,
						ChatUserID = user.Id,
						Rank = PermissionRank.USER.Ordinal,
						IsOnlineInGroup = false,
						IsActiveInGroup = false,
						Group = group,
						ChatUser = user
					};
					_context.Membership.Add(membership);
					_context.SaveChanges();
					_logger.LogInformation("User created a new account with password.");

					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var callbackUrl = Url.Page(
						"/Account/ConfirmEmail",
						pageHandler: null,
						values: new { userId = user.Id, code = code },
						protocol: Request.Scheme);

					await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
						$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

					await _signInManager.SignInAsync(user, isPersistent: false);
					return LocalRedirect(returnUrl);
				}

				foreach (var error in result.Errors) {
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			// If we got this far, something failed, redisplay form
			return Page();
		}
	}
}

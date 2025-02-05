﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Extensions;
using Chat_V2.Interfaces;
using Chat_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chat_V2.Areas.Identity.Pages.Account.Manage {
	[Authorize]
	public partial class IndexModel : PageModel {
		private readonly ChatContext _context;
		private readonly UserManager<ChatUser> _userManager;
		private readonly SignInManager<ChatUser> _signInManager;
		private readonly IEmailSender _emailSender;
		private readonly IFileOperationProvider _fileOperationProvider;

		public IndexModel(ChatContext context, UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, IEmailSender emailSender, IFileOperationProvider fileOperationProvider) {
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
			_fileOperationProvider = fileOperationProvider;
		}

		public bool IsEmailConfirmed { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		[Display(Name = "Profile Image")]
		public IFormFile ProfileImage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel {

			[Required]
			[Display(Name = "Username")]
			public string Username { get; set; }

			[Required]
			[EmailAddress]
			public string Email { get; set; }

			[Display(Name = "First Name")]
			public string FirstName { get; set; }

			[Display(Name = "Last Name")]
			public string LastName { get; set; }

			[Phone]
			[Display(Name = "Phone number")]
			public string PhoneNumber { get; set; }

			[Display(Name = "Profile Description")]
			public string ProfileDescription { get; set; }

		}

		public async Task<IActionResult> OnGetAsync() {
			var user = await _userManager.GetUserAsync(User);
			if (user == null) {
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			var userName = await _userManager.GetUserNameAsync(user);
			var email = await _userManager.GetEmailAsync(user);
			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

			Input = new InputModel {
				Username = userName,
				Email = email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = phoneNumber,
				ProfileDescription = user.ProfileDescription
			};

			IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

			return Page();
		}

		public async Task<IActionResult> OnPostUpdateAsync() {
			if (!ModelState.IsValid) {
				return Page();
			}

			var user = await _userManager.GetUserAsync(User);
			if (user == null) {
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			var username = await _userManager.GetUserNameAsync(user);
			if (Input.Username != username) {
				var setUsernameResult = await _userManager.SetUserNameAsync(user, Input.Username);
				if (!setUsernameResult.Succeeded) {
					var userId = await _userManager.GetUserIdAsync(user);
					throw new InvalidOperationException($"Unexpected error occurred setting username for user with ID '{userId}'.");
				}
			}

			var email = await _userManager.GetEmailAsync(user);
			if (Input.Email != email) {
				var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
				if (!setEmailResult.Succeeded) {
					var userId = await _userManager.GetUserIdAsync(user);
					throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
				}
			}

			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
			if (Input.PhoneNumber != phoneNumber) {
				var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
				if (!setPhoneResult.Succeeded) {
					var userId = await _userManager.GetUserIdAsync(user);
					throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
				}
			}

			if (!Input.FirstName.Equals(user.FirstName)) {
				user.FirstName = Input.FirstName;
			}

			if (!Input.LastName.Equals(user.LastName)) {
				user.LastName = Input.LastName;
			}

			if (!Input.ProfileDescription.Equals(user.ProfileDescription)) {
				user.ProfileDescription = Input.ProfileDescription;
			}

			await _context.SaveChangesAsync();
			await _signInManager.RefreshSignInAsync(user);
			StatusMessage = "Your profile has been updated";
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostUploadAsync() {
			var chatUser = await _userManager.GetUserAsync(User);

			if (ProfileImage.Length < 10485760L) {//10 MB
				if (!ProfileImage.ValidateFileTypeAsImage()) {
					ModelState.AddModelError("File", "Invalid file type.");
				} else {
					using Stream memoryStream = ProfileImage.OpenReadStream();
					Image image = Image.FromStream(memoryStream).ResizeImageToFitSquare(512);

					string output = _fileOperationProvider.SaveImageToFile(image, ProfileImage.Name);
					_fileOperationProvider.DeleteFile(chatUser.ProfileImage);
					chatUser.ProfileImage = output;
					await _context.SaveChangesAsync();
				}
			} else {
				ModelState.AddModelError("File", "The file is too large.");
			}

			return Page();
		}

		public async Task<IActionResult> OnPostSendVerificationEmailAsync() {
			if (!ModelState.IsValid) {
				return Page();
			}

			var user = await _userManager.GetUserAsync(User);
			if (user == null) {
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}


			var userId = await _userManager.GetUserIdAsync(user);
			var email = await _userManager.GetEmailAsync(user);
			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			var callbackUrl = Url.Page(
				"/Account/ConfirmEmail",
				pageHandler: null,
				values: new { userId = userId, code = code },
				protocol: Request.Scheme);
			await _emailSender.SendEmailAsync(
				email,
				"Confirm your email",
				$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

			StatusMessage = "Verification email sent. Please check your email.";
			return RedirectToPage();
		}
	}
}

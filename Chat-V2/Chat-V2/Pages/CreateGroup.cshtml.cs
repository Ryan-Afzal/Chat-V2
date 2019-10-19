using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Chat_V2.Pages {
	[Authorize]
	public class CreateGroupModel : PageModel {
		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<CreateGroupModel> _logger;
		private readonly IWebHostEnvironment _env;

		public CreateGroupModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<CreateGroupModel> logger, IWebHostEnvironment env) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
			_env = env;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel {

			[Required]
			[Display(Name = "Group Name")]
			public string Name { get; set; }

			[Display(Name = "Description")]
			public string Description { get; set; }

			[Display(Name = "Public Group?")]
			public bool IsPublic { get; set; }
		}

		public async Task<IActionResult> OnGetAsync() {
			Input = new InputModel();
			return Page();
		}

		public async Task<IActionResult> OnPostAsync() {
			if (_signInManager.IsSignedIn(User)) {
				var chatUser = await _userManager.GetUserAsync(User);

				Group group = new Group() {
					Name = Input.Name,
					Description = Input.Description,
					DateCreated = DateTime.Now,
					IsArchived = false,
					IsPrivate = !Input.IsPublic,
					GroupImage = FileTools.SaveFileFromDefault(FileTools.DefaultGroupImage)
				};

				await _context.Group.AddAsync(group);
				await _context.SaveChangesAsync();

				Membership membership = new Membership() {
					ChatUserID = chatUser.Id,
					GroupID = group.GroupID,
					IsActiveInGroup = false,
					IsOnlineInGroup = false,
					Rank = PermissionRank.OWNER.Ordinal,
					MembershipStatus = new MembershipStatus() {
						DateIssued = DateTime.Now,
						Expiration = DateTime.Now,
						Type = MembershipStatusType.NONE
					}
				};

				await _context.Membership.AddAsync(membership);
				await _context.SaveChangesAsync();

				return LocalRedirect("/group?groupId=" + group.GroupID);
			} else {
				return LocalRedirect("/Identity/Account/Login");
			}
		}
	}
}

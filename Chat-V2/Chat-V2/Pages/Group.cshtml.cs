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

namespace Chat_V2.Pages {
	public class GroupModel : PageModel {

		public class GroupViewModel {
			public bool IsGroupMember { get; set; }
			public ChatUser ChatUser { get; set; }
			public Group Group { get; set; }
			public Membership Membership { get; set; }
		}

		public class JoinGroupInputModel {
			public string Message { get; set; }
		}

		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<ChatModel> _logger;

		public GroupModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<ChatModel> logger) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
		}

		/// <summary>
		/// Initial data goes here. DATA SHOULD NEVER BE SAVED TO THE VIEWMODEL!!!!!!!!!!
		/// </summary>
		[BindProperty]
		public GroupViewModel ViewModel { get; private set; }

		[BindProperty]
		public JoinGroupInputModel JoinGroupInput { get; private set; }

		public async Task<IActionResult> OnGetAsync(int? groupId) {
			if (groupId == null) {
				return LocalRedirect("/");
			}

			if (_signInManager.IsSignedIn(User)) {
				var chatUser = await _userManager.GetUserAsync(User);
				var group = await _context.Group
							.Include(g => g.GroupJoinRequests)
							.Include(g => g.Memberships)
							.FirstOrDefaultAsync(g => g.GroupID == groupId);
				var membership = group.Memberships
							.FirstOrDefault(m => m.ChatUserID == chatUser.Id);

				ViewModel = new GroupViewModel();
				JoinGroupInput = new JoinGroupInputModel();

				if (membership == null) {
					if (group.IsPrivate) {
						return LocalRedirect("/");
					} else {
						ViewModel.IsGroupMember = false;
					}
				} else {
					ViewModel.IsGroupMember = true;
					ViewModel.ChatUser = chatUser;
					ViewModel.Group = group;
					ViewModel.Membership = membership;
				}

				return Page();
			} else {
				return LocalRedirect("/Identity/Account/Login");
			}
		}

		public async Task<IActionResult> OnPostSendJoinRequestAsync() {
			GroupJoinRequest request = new GroupJoinRequest() {
				ChatUserID = ViewModel.ChatUser.Id,
				GroupID = ViewModel.Group.GroupID,
				Message = JoinGroupInput.Message,
				DateSent = DateTime.Now
			};

			ViewModel.GroupJoinRequests.Add(request);
			await _context.SaveChangesAsync();

			throw new NotImplementedException();
		}
	}
}
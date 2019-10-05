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
			public bool JoinRequestSent { get; set; }
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

		[BindProperty]
		public GroupViewModel ViewModel { get; set; }

		[BindProperty]
		public JoinGroupInputModel JoinGroupInput { get; set; }

		public async Task<IActionResult> OnGetAsync(int? groupId) {
			if (groupId == null) {
				return LocalRedirect("/");
			}

			if (_signInManager.IsSignedIn(User)) {
				var chatUser = await _userManager.GetUserAsync(User);
				var group = await _context.Group
							.Include(g => g.GroupJoinRequests)
								.ThenInclude(r => r.ChatUser)
							.Include(g => g.Memberships)
								.ThenInclude(m => m.ChatUser)
							.FirstOrDefaultAsync(g => g.GroupID == groupId);

				if (group == null) {
					return LocalRedirect("/");
				}

				var membership = group.Memberships
							.FirstOrDefault(m => m.ChatUserID == chatUser.Id);

				ViewModel = new GroupViewModel() {
					Group = group,
					JoinRequestSent = JoinRequestSent(group, chatUser.Id),
					ChatUser = chatUser
				};
				JoinGroupInput = new JoinGroupInputModel();

				if (membership == null) {
					if (group.IsPrivate) {
						return LocalRedirect("/");
					} else {
						ViewModel.IsGroupMember = false;
					}
				} else {
					ViewModel.IsGroupMember = true;
					ViewModel.Membership = membership;
				}

				return Page();
			} else {
				return LocalRedirect("/Identity/Account/Login");
			}
		}

		public async Task<IActionResult> OnPostSendJoinRequestAsync(int? userId, int? groupId) {
			if (userId == null || groupId == null) {
				return LocalRedirect("/");
			}

			GroupJoinRequest request = new GroupJoinRequest() {
				ChatUserID = (int)userId,
				GroupID = (int)groupId,
				Message = JoinGroupInput.Message,
				DateSent = DateTime.Now
			};

			(await _context.Group
				.Include(g => g.GroupJoinRequests)
				.FirstOrDefaultAsync(g => g.GroupID == request.GroupID))
				.GroupJoinRequests
				.Add(request);
			await _context.SaveChangesAsync();

			return LocalRedirect("/group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostLeaveGroupAsync(int? userId, int? groupId) {
			if (userId == null || groupId == null) {
				return LocalRedirect("/");
			}

			if ((await _userManager.GetUserAsync(User)).Id != userId) {
				return BadRequest();
			}

			Group group = await _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			Membership membership = group.Memberships.FirstOrDefault(m => m.ChatUserID == userId);

			_context.Membership.Remove(membership);
			await _context.SaveChangesAsync();

			return LocalRedirect("/group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostAcceptJoinRequestAsync(int? groupId, int? requestId) {
			if (requestId == null || groupId == null) {
				return LocalRedirect("/");
			}

			var group = await _context.Group
				.Include(g => g.GroupJoinRequests)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				return LocalRedirect("/");
			}

			var request = group.GroupJoinRequests
				.FirstOrDefault(r => r.GroupJoinRequestID == requestId);

			if (request == null) {
				return LocalRedirect("/");
			}

			Membership membership = new Membership() {
				ChatUserID = request.ChatUserID,
				GroupID = group.GroupID,
				IsActiveInGroup = false,
				IsOnlineInGroup = false,
				MembershipStatus = new MembershipStatus() {
					DateIssued = DateTime.Now,
					Expiration = DateTime.Now,
					Type = MembershipStatusType.NONE
				},
				Rank = PermissionRank.USER.Ordinal
			};

			await _context.Membership.AddAsync(membership);
			group.GroupJoinRequests.Remove(request);
			await _context.SaveChangesAsync();

			return LocalRedirect("/group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostRejectJoinRequestAsync(int? groupId, int? requestId) {
			if (requestId == null || groupId == null) {
				return LocalRedirect("/");
			}

			var group = await _context.Group
				.Include(g => g.GroupJoinRequests)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				return LocalRedirect("/");
			}

			var request = group.GroupJoinRequests
				.FirstOrDefault(r => r.GroupJoinRequestID == requestId);

			if (request == null) {
				return LocalRedirect("/");
			}

			group.GroupJoinRequests.Remove(request);
			await _context.SaveChangesAsync();

			return LocalRedirect("/group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostMakePrivateAsync(int? groupId) {
			throw new NotImplementedException();
		}

		public async Task<IActionResult> OnPostMakePublicAsync(int? groupId) {
			throw new NotImplementedException();
		}

		private bool JoinRequestSent(Group group, int userId) {
			return group.GroupJoinRequests.FirstOrDefault(r => r.ChatUserID == userId) != null;
		}
	}
}
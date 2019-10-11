using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
	public class GroupModel : PageModel {

		public class GroupViewModel {
			public bool IsGroupMember { get; set; }
			public bool IsBanned { get; set; }
			public bool JoinRequestSent { get; set; }
			public ChatUser ChatUser { get; set; }
			public Group Group { get; set; }
			public Membership Membership { get; set; }
		}

		public class JoinGroupInputModel {
			public string Message { get; set; }
		}

		public class InviteToGroupInputModel {
			[Required]
			public int UserID { get; set; }

			[Required]
			public string UserName { get; set; }
			
			public string Message { get; set; }
		}

		public class ChangeOwnerInputModel {
			public int GroupID { get; set; }
			public int UserID { get; set; }
			public string UserName { get; set; }
		}

		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<GroupModel> _logger;

		public GroupModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<GroupModel> logger) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
		}

		[BindProperty]
		public GroupViewModel ViewModel { get; set; }

		[BindProperty]
		public JoinGroupInputModel JoinGroupInput { get; set; }

		[BindProperty]
		public InviteToGroupInputModel InviteToGroupInput { get; set; }

		[BindProperty]
		public ChangeOwnerInputModel ChangeOwnerInput { get; set; }

		[BindProperty]
		public IFormFile GroupImage { get; set; }

		public async Task<IActionResult> OnGetAsync(int? groupId) {
			if (groupId == null) {
				return NotFound();
			}

			var chatUser = await _userManager.GetUserAsync(User);
			var group = await _context.Group
						.Include(g => g.GroupJoinRequests)
							.ThenInclude(r => r.ChatUser)
						.Include(g => g.BannedUsers)
						.Include(g => g.Memberships)
							.ThenInclude(m => m.ChatUser)
						.Include(g => g.GroupImage)
						.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				return NotFound();
			}

			var membership = group.Memberships
						.FirstOrDefault(m => m.ChatUserID == chatUser.Id);

			ViewModel = new GroupViewModel() {
				Group = group,
				JoinRequestSent = JoinRequestSent(group, chatUser.Id),
				ChatUser = chatUser
			};

			JoinGroupInput = new JoinGroupInputModel();
			InviteToGroupInput = new InviteToGroupInputModel();
			ChangeOwnerInput = new ChangeOwnerInputModel();

			if (membership == null) {
				if (group.IsPrivate) {
					return LocalRedirect("/Groups");
				} else {
					ViewModel.IsGroupMember = false;
					if (group.BannedUsers.FirstOrDefault(u => u.Id == chatUser.Id) == null) {
						ViewModel.IsBanned = false;
					} else {
						ViewModel.IsBanned = true;
					}
				}
			} else {
				ViewModel.IsGroupMember = true;
				ViewModel.IsBanned = false;
				ViewModel.Membership = membership;
			}

			return Page();
		}

		public async Task<IActionResult> OnPostSendJoinRequestAsync(int? userId, int? groupId) {
			if (userId == null || groupId == null) {
				return NotFound();
			}

			var group = await _context.Group
				.Include(g => g.GroupJoinRequests)
				.Include(g => g.BannedUsers)
				.FirstOrDefaultAsync(g => g.GroupID == groupId.Value);

			if (group.BannedUsers.FirstOrDefault(u => u.Id == userId.Value) != null) {
				return BadRequest();
			}

			GroupJoinRequest request = new GroupJoinRequest() {
				ChatUserID = userId.Value,
				GroupID = group.GroupID,
				Message = JoinGroupInput.Message,
				DateSent = DateTime.Now
			};

			group.GroupJoinRequests.Add(request);
			await _context.SaveChangesAsync();

			return LocalRedirect("/group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostLeaveGroupAsync(int? userId, int? groupId) {
			if (userId == null || groupId == null) {
				return NotFound();
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
				return NotFound();
			}

			var group = await _context.Group
				.Include(g => g.GroupJoinRequests)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				return NotFound();
			}

			var request = group.GroupJoinRequests
				.FirstOrDefault(r => r.GroupJoinRequestID == requestId);

			if (request == null) {
				return NotFound();
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
				return NotFound();
			}

			var group = await _context.Group
				.Include(g => g.GroupJoinRequests)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				return NotFound();
			}

			var request = group.GroupJoinRequests
				.FirstOrDefault(r => r.GroupJoinRequestID == requestId);

			if (request == null) {
				return NotFound();
			}

			group.GroupJoinRequests.Remove(request);
			await _context.SaveChangesAsync();

			return LocalRedirect("/group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostInviteToGroupAsync(int? groupId) {
			if (groupId == null) {
				return LocalRedirect("/");
			}

			var chatUser = await _context.Users
				.Include(u => u.GroupJoinInvitations)
				.Include(u => u.Memberships)
				.FirstOrDefaultAsync(u => u.Id == InviteToGroupInput.UserID);

			if (chatUser == null) {
				return LocalRedirect("/group?groupId=" + groupId);
			}

			if (!chatUser.UserName.Equals(InviteToGroupInput.UserName)) {
				return LocalRedirect("/group?groupId=" + groupId);
			}

			var group = await _context.Group
				.Include(g => g.BannedUsers)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				return NotFound();
			}

			if (chatUser.GroupJoinInvitations.FirstOrDefault(i => i.GroupID == groupId.Value) != null
				|| chatUser.Memberships.FirstOrDefault(m => m.GroupID == groupId) != null
				|| group.BannedUsers.FirstOrDefault(u => u.Id == chatUser.Id) != null) {
				return LocalRedirect("/group?groupId=" + groupId);
			}

			GroupJoinInvitation invitation = new GroupJoinInvitation() {
				GroupID = groupId.Value,
				DateSent = DateTime.Now,
				Message = InviteToGroupInput.Message
			};

			chatUser.GroupJoinInvitations.Add(invitation);

			await _context.SaveChangesAsync();
			return LocalRedirect("/group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostUnbanUserAsync(int? userId, int? groupId) {
			if (userId == null || groupId == null) {
				return BadRequest();
			}

			return LocalRedirect("/ConfirmUnbanUser?userId=" + userId + "&groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostBanUserAsync(int? userId, int? groupId) {
			if (userId == null || groupId == null) {
				return BadRequest();
			}

			return LocalRedirect("/ConfirmBanUser?userId=" + userId + "&groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostUploadImageAsync(int? groupId) {
			if (groupId == null) {
				return BadRequest();
			}

			var group = await _context.Group.FirstOrDefaultAsync(g => g.GroupID == groupId.Value);

			using (Stream memoryStream = GroupImage.OpenReadStream()) {
				if (memoryStream.Length < 10485760) {
					var appImage = await _context.GroupImage.FirstOrDefaultAsync(i => i.GroupImageID == group.GroupImageID);

					Image image = Image.FromStream(memoryStream);

					double aspectRatio = ((double)image.Height) / image.Width;

					int height;
					int width;

					if (image.Height > image.Width) {
						height = image.Height > 512 ? 512 : image.Height;
						width = (int)(height / aspectRatio);
					} else {
						width = image.Width > 512 ? 512 : image.Width;
						height = (int)(aspectRatio * width);
					}

					using MemoryStream ms = new MemoryStream();
					image.ResizeImage(height, width)
						.Save(ms, ImageFormat.Png);

					appImage.Data = ms.ToArray();
					appImage.ContentType = "png";

					await _context.SaveChangesAsync();
				} else {
					ModelState.AddModelError("File", "The file is too large.");
				}
			}

			return LocalRedirect("/Group?groupId=" + groupId.Value);
		}

		public async Task<IActionResult> OnPostMakePrivateAsync(int? groupId) {
			if (groupId == null) {
				return LocalRedirect("/");
			}

			var chatUser = await _userManager.GetUserAsync(User);
			Group group = await _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			Membership membership = group.Memberships.FirstOrDefault(m => m.ChatUserID == chatUser.Id);

			if (membership == null || membership.Rank < PermissionRank.OWNER.Ordinal) {
				return BadRequest();
			}

			group.IsPrivate = true;
			
			await _context.SaveChangesAsync();

			return LocalRedirect("/group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostMakePublicAsync(int? groupId) {
			if (groupId == null) {
				return LocalRedirect("/");
			}

			var chatUser = await _userManager.GetUserAsync(User);
			Group group = await _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			Membership membership = group.Memberships.FirstOrDefault(m => m.ChatUserID == chatUser.Id);

			if (membership == null || membership.Rank < PermissionRank.OWNER.Ordinal) {
				return BadRequest();
			}

			group.IsPrivate = false;

			await _context.SaveChangesAsync();

			return LocalRedirect("/group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostChangeOwnerAsync() {
			throw new NotImplementedException();
		}

		public async Task<IActionResult> OnPostArchiveGroupAsync(int? groupId) {
			throw new NotImplementedException();
		}

		public async Task<IActionResult> OnPostDeleteGroupAsync(int? groupId) {
			throw new NotImplementedException();
		}

		private bool JoinRequestSent(Group group, int userId) {
			return group.GroupJoinRequests.FirstOrDefault(r => r.ChatUserID == userId) != null;
		}
	}
}
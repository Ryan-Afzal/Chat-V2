using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Extensions;
using Chat_V2.Interfaces;
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

		public class SendJoinRequestInputModel {
			public int ChatUserID { get; set; }
			public int GroupID { get; set; }
			public string Message { get; set; }
		}

		public class LeaveGroupInputModel {
			public int ChatUserID { get; set; }
			public int GroupID { get; set; }
		}

		public class JoinRequestInputModel {
			public int GroupID { get; set; }
			public int RequestID { get; set; }
		}

		public class InviteToGroupInputModel {
			[Required]
			public int GroupID { get; set; }
			[Required]
			public int UserID { get; set; }
			[Required]
			public string UserName { get; set; }
			public string Message { get; set; }
		}

		public class UnbanUserInputModel {
			public int ChatUserID { get; set; }
			public int GroupID { get; set; }
		}

		public class BanUserInputModel {
			public int ChatUserID { get; set; }
			public int GroupID { get; set; }
		}

		public class ChangeGroupNameInputModel {
			[Required]
			public int GroupID { get; set; }
			[Required]
			[MinLength(length: 1)]
			public string Name { get; set; }
		}

		public class ChangeGroupDescriptionInputModel {
			[Required]
			public int GroupID { get; set; }
			[Required]
			public string Description { get; set; }
		}

		public class UploadImageInputModel {
			public int GroupID { get; set; }
			public IFormFile GroupImage { get; set; }
		}

		public class PublicPrivateInputModel {
			public int GroupID { get; set; }
		}

		public class DangerZoneInputModel {
			public int GroupID { get; set; }
		}

		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<GroupModel> _logger;
		private readonly IFileOperationProvider _fileOperationProvider;

		public GroupModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<GroupModel> logger, IFileOperationProvider fileOperationProvider) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
			_fileOperationProvider = fileOperationProvider;
		}

		[BindProperty]
		public GroupViewModel ViewModel { get; set; }

		[BindProperty]
		public SendJoinRequestInputModel SendJoinRequestInput { get; set; }

		[BindProperty]
		public LeaveGroupInputModel LeaveGroupInput { get; set; }

		[BindProperty]
		public JoinRequestInputModel JoinRequestInput { get; set; }

		[BindProperty]
		public InviteToGroupInputModel InviteToGroupInput { get; set; }

		[BindProperty]
		public UnbanUserInputModel UnbanUserInput { get; set; }

		[BindProperty]
		public BanUserInputModel BanUserInput { get; set; }

		[BindProperty]
		public ChangeGroupNameInputModel ChangeGroupNameInput { get; set; }

		[BindProperty]
		public ChangeGroupDescriptionInputModel ChangeGroupDescriptionInput { get; set; }

		[BindProperty]
		public UploadImageInputModel UploadImageInput { get; set; }

		[BindProperty]
		public PublicPrivateInputModel PublicPrivateInput { get; set; }

		[BindProperty]
		public DangerZoneInputModel DangerZoneInput { get; set; }

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

			SendJoinRequestInput = new SendJoinRequestInputModel();
			LeaveGroupInput = new LeaveGroupInputModel();
			JoinRequestInput = new JoinRequestInputModel();
			InviteToGroupInput = new InviteToGroupInputModel();
			UnbanUserInput = new UnbanUserInputModel();
			BanUserInput = new BanUserInputModel();
			UploadImageInput = new UploadImageInputModel();
			PublicPrivateInput = new PublicPrivateInputModel();

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

		public async Task<IActionResult> OnPostSendJoinRequestAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			var group = await _context.Group
				.Include(g => g.GroupJoinRequests)
				.Include(g => g.BannedUsers)
				.FirstOrDefaultAsync(g => g.GroupID == SendJoinRequestInput.GroupID);

			if (group.BannedUsers.FirstOrDefault(u => u.Id == SendJoinRequestInput.ChatUserID) != null) {
				return BadRequest();
			}

			GroupJoinRequest request = new GroupJoinRequest() {
				ChatUserID = SendJoinRequestInput.ChatUserID,
				GroupID = group.GroupID,
				Message = SendJoinRequestInput.Message,
				DateSent = DateTime.Now
			};

			group.GroupJoinRequests.Add(request);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostLeaveGroupAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			if ((await _userManager.GetUserAsync(User)).Id != LeaveGroupInput.ChatUserID) {
				return BadRequest();
			}

			Group group = await _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == LeaveGroupInput.GroupID);

			Membership membership = group.Memberships.FirstOrDefault(m => m.ChatUserID == LeaveGroupInput.ChatUserID);

			_context.Membership.Remove(membership);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostAcceptJoinRequestAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var group = await _context.Group
				.Include(g => g.GroupJoinRequests)
				.FirstOrDefaultAsync(g => g.GroupID == JoinRequestInput.GroupID);

			if (group == null) {
				return NotFound();
			}

			var request = group.GroupJoinRequests
				.FirstOrDefault(r => r.GroupJoinRequestID == JoinRequestInput.RequestID);

			if (request == null) {
				return NotFound();
			}

			Membership membership = new Membership() {
				ChatUserID = request.ChatUserID,
				GroupID = group.GroupID,
				IsActiveInGroup = false,
				IsOnlineInGroup = false,
				Rank = PermissionRank.USER.Ordinal,
				LastViewedMessageID = null,
				NumNew = 0
			};

			await _context.Membership.AddAsync(membership);
			group.GroupJoinRequests.Remove(request);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostRejectJoinRequestAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var group = await _context.Group
				.Include(g => g.GroupJoinRequests)
				.FirstOrDefaultAsync(g => g.GroupID == JoinRequestInput.GroupID);

			if (group == null) {
				return NotFound();
			}

			var request = group.GroupJoinRequests
				.FirstOrDefault(r => r.GroupJoinRequestID == JoinRequestInput.RequestID);

			if (request == null) {
				return NotFound();
			}

			group.GroupJoinRequests.Remove(request);
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostInviteToGroupAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			var chatUser = await _context.Users
				.Include(u => u.GroupJoinInvitations)
				.Include(u => u.Memberships)
				.FirstOrDefaultAsync(u => u.Id == InviteToGroupInput.UserID);

			if (chatUser == null) {
				return LocalRedirect(returnUrl);
			}

			if (!chatUser.UserName.Equals(InviteToGroupInput.UserName)) {
				return LocalRedirect(returnUrl);
			}

			var group = await _context.Group
				.Include(g => g.BannedUsers)
				.FirstOrDefaultAsync(g => g.GroupID == InviteToGroupInput.GroupID);

			if (group == null) {
				return NotFound();
			}

			if (chatUser.GroupJoinInvitations.FirstOrDefault(i => i.GroupID == group.GroupID) != null
				|| chatUser.Memberships.FirstOrDefault(m => m.GroupID == group.GroupID) != null
				|| group.BannedUsers.FirstOrDefault(u => u.Id == chatUser.Id) != null) {
				return LocalRedirect(returnUrl);
			}

			GroupJoinInvitation invitation = new GroupJoinInvitation() {
				GroupID = group.GroupID,
				DateSent = DateTime.Now,
				Message = InviteToGroupInput.Message
			};

			chatUser.GroupJoinInvitations.Add(invitation);

			await _context.SaveChangesAsync();
			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostUnbanUserAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			return LocalRedirect("/ConfirmUnbanUser?userId=" + UnbanUserInput.ChatUserID + "&groupId=" + UnbanUserInput.GroupID + "&returnUrl=" + returnUrl);
		}

		public async Task<IActionResult> OnPostBanUserAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			if (_userManager.GetUserId(User).Equals(BanUserInput.ChatUserID + "")) {
				return LocalRedirect(returnUrl);
			}

			return LocalRedirect("/ConfirmBanUser?userId=" + BanUserInput.ChatUserID + "&groupId=" + BanUserInput.GroupID + "&returnUrl=" + returnUrl);
		}

		public async Task<IActionResult> OnPostChangeGroupNameAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			var chatUser = await _userManager.GetUserAsync(User);
			Group group = await _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == ChangeGroupNameInput.GroupID);

			Membership membership = group.Memberships.FirstOrDefault(m => m.ChatUserID == chatUser.Id);

			if (membership == null || membership.Rank < PermissionRank.ADMINISTRATOR.Ordinal) {
				return BadRequest();
			}

			group.Name = ChangeGroupNameInput.Name;

			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostChangeGroupDescriptionAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			var chatUser = await _userManager.GetUserAsync(User);
			Group group = await _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == ChangeGroupDescriptionInput.GroupID);

			Membership membership = group.Memberships.FirstOrDefault(m => m.ChatUserID == chatUser.Id);

			if (membership == null || membership.Rank < PermissionRank.ADMINISTRATOR.Ordinal) {
				return BadRequest();
			}

			group.Description = ChangeGroupDescriptionInput.Description;

			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostUploadImageAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			var group = await _context.Group.FirstOrDefaultAsync(g => g.GroupID == UploadImageInput.GroupID);

			if (UploadImageInput.GroupImage.Length < 10485760L) {//10 MB
				if (!UploadImageInput.GroupImage.ValidateFileTypeAsImage()) {
					ModelState.AddModelError("File", "Invalid file type.");
				} else {
					using Stream memoryStream = UploadImageInput.GroupImage.OpenReadStream();
					Image image = Image.FromStream(memoryStream).ResizeImageToFitSquare(512);

					string output = _fileOperationProvider.SaveImageToFile(image, UploadImageInput.GroupImage.Name);
					_fileOperationProvider.DeleteFile(group.GroupImage);
					group.GroupImage = output;
					await _context.SaveChangesAsync();
				}
			} else {
				ModelState.AddModelError("File", "The file is too large.");
			}

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostMakePrivateAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			var chatUser = await _userManager.GetUserAsync(User);
			Group group = await _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == PublicPrivateInput.GroupID);

			Membership membership = group.Memberships.FirstOrDefault(m => m.ChatUserID == chatUser.Id);

			if (membership == null || membership.Rank < PermissionRank.OWNER.Ordinal) {
				return BadRequest();
			}

			group.IsPrivate = true;
			
			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostMakePublicAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			var chatUser = await _userManager.GetUserAsync(User);
			Group group = await _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == PublicPrivateInput.GroupID);

			Membership membership = group.Memberships.FirstOrDefault(m => m.ChatUserID == chatUser.Id);

			if (membership == null || membership.Rank < PermissionRank.OWNER.Ordinal) {
				return BadRequest();
			}

			group.IsPrivate = false;

			await _context.SaveChangesAsync();

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostChangeOwnerAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			throw new NotImplementedException();
		}

		public async Task<IActionResult> OnPostArchiveGroupAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");
			throw new NotImplementedException();
		}

		public async Task<IActionResult> OnPostDeleteGroupAsync(string returnUrl = null) {
			returnUrl ??= Url.Content("~/");

			return LocalRedirect("/ConfirmDeleteGroup?groupId=" + DangerZoneInput.GroupID + "&returnUrl=" + returnUrl);
		}

		private bool JoinRequestSent(Group group, int userId) {
			return group.GroupJoinRequests.FirstOrDefault(r => r.ChatUserID == userId) != null;
		}
	}
}
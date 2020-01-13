using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Hubs;
using Chat_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat_V2.Pages {
	[Authorize]
	public class ConfirmUnbanUserModel : PageModel {
		private readonly IHubContext<NotifHub> _hubContext;
		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<ConfirmUnbanUserModel> _logger;

		public ConfirmUnbanUserModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<ConfirmUnbanUserModel> logger, IHubContext<NotifHub> hubContext) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
			_hubContext = hubContext;
		}

		[BindProperty]
		public int UserID { get; set; }
		[BindProperty]
		public string UserName { get; set; }
		[BindProperty]
		public int GroupID { get; set; }
		
		public async Task<IActionResult> OnGetAsync(int? userId, int? groupId) {
			if (userId == null || groupId == null) {
				return BadRequest();
			}

			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null) {
				return NotFound();
			}

			var currentUser = await _userManager.GetUserAsync(User);

			var group = await _context.Group
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				return NotFound();
			}

			var current = group.Memberships
				.OfType<MultiuserGroupMembership>()
				.FirstOrDefault(m => m.ChatUserID == currentUser.Id);

			if (current == null || current.Rank < PermissionRank.OFFICER.Ordinal) {
				return BadRequest();
			}

			UserID = user.Id;
			UserName = user.UserName;
			GroupID = group.GroupID;

			return Page();
		}

		public async Task<IActionResult> OnPostCancelAsync(int? groupId) {
			if (groupId == null) {
				return BadRequest();
			}

			return LocalRedirect("/Group?groupId=" + groupId);
		}

		public async Task<IActionResult> OnPostConfirmAsync(int? userId, int? groupId) {
			if (groupId == null || userId == null) {
				return BadRequest();
			}

			var group = await _context.Group
				.OfType<MultiuserGroup>()
				.Include(g => g.Memberships)
				.Include(g => g.BannedUsers)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			var chatUser = await _context.Users
				.Include(u => u.Notifications)
				.FirstOrDefaultAsync(u => u.Id == userId.Value);
			var currentUser = await _userManager.GetUserAsync(User);
			var currentMembership = group.Memberships
				.OfType<MultiuserGroupMembership>()
				.FirstOrDefault(m => m.ChatUserID == currentUser.Id);

			if (currentMembership is null || currentMembership.Rank < PermissionRank.OFFICER.Ordinal) {
				return BadRequest();
			}

			group.BannedUsers.Remove(chatUser);

			Notification notif = new Notification() {
				ChatUserID = chatUser.Id,
				Date = DateTime.Now,
				Title = $"No longer banned from {group.Name}",
				Text = $"You are no longer banned from {group.Name}.",
				ViewURL = $"/Group?groupId={group.GroupID}"
			};

			chatUser.Notifications.Add(notif);
			await _context.SaveChangesAsync();

			IClientProxy proxy = _hubContext.Clients.User(chatUser.Id + "");

			await proxy.SendAsync("NewNotification",
				new NewNotificationArgs() {
					ChatUserID = notif.ChatUserID
				});

			await proxy.SendAsync("ReceiveNotification",
				new ReceiveNotificationArgs() {
					ChatUserID = notif.ChatUserID,
					NotificationID = notif.NotificationID,
					Date = notif.Date.ToString(),
					Title = notif.Title,
					Text = notif.Text,
					ViewURL = notif.ViewURL
				});

			return LocalRedirect("/Group?groupId=" + groupId);
		}
	}
}
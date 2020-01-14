using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Extensions;
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
	public class ConfirmBanUserModel : PageModel {
		private readonly IHubContext<NotifHub> _hubContext;
		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<ConfirmBanUserModel> _logger;

		public ConfirmBanUserModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<ConfirmBanUserModel> logger, IHubContext<NotifHub> hubContext) {
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
		[BindProperty]
		public int? MembershipID { get; set; }
		
		public async Task<IActionResult> OnGetAsync(int? userId, int? groupId) {
			if (userId == null || groupId == null) {
				return BadRequest();
			}

			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null) {
				return NotFound();
			}

			var currentUser = await _userManager.GetUserAsync(User);

			if (currentUser.Id == userId.Value) {
				return BadRequest();
			}

			var group = await _context.Group
				.OfType<MultiuserGroup>()
				.Include(g => g.Memberships)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			if (group == null) {
				return NotFound();
			}

			var memberships = group.Memberships
				.OfType<MultiuserGroupMembership>()
				.Where(m => m.ChatUserID == user.Id || m.ChatUserID == currentUser.Id);

			MultiuserGroupMembership other = null;
			MultiuserGroupMembership current = null;
			
			foreach (var m in memberships) {
				if (m.ChatUserID == user.Id) {
					other = m;
				} else {
					current = m;
				}
			}

			if (current == null || current.Rank < PermissionRank.OFFICER.Ordinal) {
				return BadRequest();
			}

			if (other == null) {
				MembershipID = null;
			} else if (other.Rank >= current.Rank) {
				return BadRequest();
			} else {
				MembershipID = other.MembershipID;
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

		public async Task<IActionResult> OnPostConfirmAsync(int? userId, int? groupId, int? membershipId) {
			if (groupId is null || userId is null) {
				return BadRequest();
			}

			var group = await _context.Group
				.OfType<MultiuserGroup>()
				.Include(g => g.Memberships)
				.Include(g => g.BannedUsers)
				.FirstOrDefaultAsync(g => g.GroupID == groupId);

			var chatUser = await _context.Users
				.FirstOrDefaultAsync(u => u.Id == userId.Value);
			var currentUser = await _userManager.GetUserAsync(User);
			var currentMembership = group.MultiuserGroupMemberships
				.FirstOrDefault(m => m.ChatUserID == currentUser.Id);

			if (currentMembership is null) {
				return BadRequest();
			}

			var membership = await _context.Membership
				.OfType<MultiuserGroupMembership>()
				.FirstOrDefaultAsync(m => m.MembershipID == membershipId);

			if (membership.ChatUserID != chatUser.Id || membership.GroupID != group.GroupID || currentMembership.Rank <= membership.Rank) {
				return BadRequest();
			}

			_context.Membership.Remove(membership);
			group.BannedUsers.Add(chatUser);

			Notification notif = new Notification() {
				ChatUserID = chatUser.Id,
				Date = DateTime.Now,
				Title = $"Banned from {group.Name}",
				Text = $"You have been banned from {group.Name}.",
				ViewURL = $"/Group?groupId={group.GroupID}"
			};

			await _context.Notification.AddAsync(notif);
			await _context.SaveChangesAsync();

			await _hubContext.SendNotificationAsync(notif);

			return LocalRedirect("/Group?groupId=" + groupId);
		}
	}
}
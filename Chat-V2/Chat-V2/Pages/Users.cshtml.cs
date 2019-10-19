using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat_V2.Pages {

	[Authorize]
	public class UsersModel : PageModel {

		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<ChatModel> _logger;

		public UsersModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<ChatModel> logger) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
		}

		public string IdSort { get; set; }
		public string NameSort { get; set; }
		public string CurrentFilter { get; set; }
		public string CurrentSort { get; set; }

		public PaginatedList<ChatUser> Users { get; set; }

		public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex) {
			CurrentSort = sortOrder;
			IdSort = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
			NameSort = sortOrder == "Name" ? "name_desc" : "Name";

			if (searchString != null) {
				pageIndex = 1;
			} else {
				searchString = currentFilter;
			}

			CurrentFilter = searchString;

			IQueryable<ChatUser> UsersIQ = _context.Users
				.Include(u => u.ProfileImage);

			if (!string.IsNullOrEmpty(searchString)) {
				UsersIQ = UsersIQ
					.Where(g => g.UserName.Contains(searchString));
			}

			switch (sortOrder) {
				case "name_desc":
					UsersIQ = UsersIQ.OrderByDescending(g => g.UserName);
					break;
				case "Name":
					UsersIQ = UsersIQ.OrderBy(g => g.UserName);
					break;
				case "id_desc":
					UsersIQ = UsersIQ.OrderByDescending(g => g.Id);
					break;
				default:
					UsersIQ = UsersIQ.OrderBy(g => g.Id);
					break;
			}

			int pageSize = 10;
			Users = await PaginatedList<ChatUser>.CreateAsync(
				UsersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
		}

	}

}
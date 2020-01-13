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
	public class GroupsModel : PageModel {

		private readonly SignInManager<ChatUser> _signInManager;
		private readonly UserManager<ChatUser> _userManager;
		private readonly ChatContext _context;
		private readonly ILogger<ChatModel> _logger;

		public GroupsModel(UserManager<ChatUser> userManager, SignInManager<ChatUser> signInManager, ChatContext context, ILogger<ChatModel> logger) {
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_logger = logger;
		}

		public string IdSort { get; set; }
		public string NameSort { get; set; }
		public string CurrentFilter { get; set; }
		public string CurrentSort { get; set; }

		public PaginatedList<MultiuserGroup> Groups { get; set; }

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

			IQueryable<MultiuserGroup> groupsIQ = _context.Group
					.OfType<MultiuserGroup>()
					.Where(g => !g.IsPrivate);

			if (!string.IsNullOrEmpty(searchString)) {
				groupsIQ = groupsIQ
					.Where(g => g.Name.Contains(searchString));
			}

			switch (sortOrder) {
				case "name_desc":
					groupsIQ = groupsIQ.OrderByDescending(g => g.Name);
					break;
				case "Name":
					groupsIQ = groupsIQ.OrderBy(g => g.Name);
					break;
				case "id_desc":
					groupsIQ = groupsIQ.OrderByDescending(g => g.GroupID);
					break;
				default:
					groupsIQ = groupsIQ.OrderBy(g => g.GroupID);
					break;
			}

			int pageSize = 10;
			Groups = await PaginatedList<MultiuserGroup>.CreateAsync(
				groupsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
		}

	}

}
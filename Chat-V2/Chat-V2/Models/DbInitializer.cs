using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat_V2.Models {

	public static class DbInitializer {

		public static void Initialize(ChatContext context) {

			//context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			var user = context.Users.FirstOrDefault(u => u.UserName.Equals("Ryan-Afzal"));

			if (user != null) {
				user.Memberships.FirstOrDefault().Rank = PermissionRank.SUPERUSER.Ordinal;
			}

			if (context.Group.Any()) {
				return;
			}

			Group globalServerGroup = new Group() {
				Name = "Global",
				DateCreated = DateTime.Now,
				IsPrivate = false,
				IsArchived = false,
				Description = "The global chat"
			};
			context.Group.Add(globalServerGroup);

			Group testGroup = new Group() {
				Name = "Test Group",
				DateCreated = DateTime.Now,
				IsPrivate = false,
				IsArchived = false,
				Description = "Ryan's Test Group"
			};
			context.Group.Add(testGroup);
			context.SaveChanges();

		}

	}

}

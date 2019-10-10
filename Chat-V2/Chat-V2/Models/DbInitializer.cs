using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat_V2.Models {

	public static class DbInitializer {

		public static void Initialize(ChatContext context) {

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			if (context.Groups.Any()) {
				return;
			}

			Group globalServerGroup = new Group() {
				Name = "Global",
				DateCreated = DateTime.Now,
				IsPrivate = false,
				IsArchived = false,
				Description = "The global chat"
			};
			context.Groups.Add(globalServerGroup);
			context.SaveChanges();

		}

	}

}

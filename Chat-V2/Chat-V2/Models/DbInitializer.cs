﻿using System;
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
				IsPrivate = false
			};

			context.Group.Add(globalServerGroup);
			context.SaveChanges();

		}

	}

}

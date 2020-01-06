using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Chat_V2.Models {

	public static class DbInitializer {

		public static void Initialize(ChatContext context, IWebHostEnvironment env, IFileOperationProvider fileConfiguration) {

			//context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			if (context.Group.Any()) {
				return;
			}

			Group globalServerGroup = new Group() {
				Name = "Global",
				DateCreated = DateTime.Now,
				IsPrivate = false,
				IsArchived = false,
				Description = "The global chat",
				GroupImage = fileConfiguration.SaveFileFromDefault(fileConfiguration.DefaultGroupImage)
			};
			context.Group.Add(globalServerGroup);
			context.SaveChanges();

		}

	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Chat_V2.Models {

	public static class DbInitializer {

		public static void Initialize(ChatContext context, IWebHostEnvironment env) {

			//context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			if (context.Group.Any()) {
				return;
			}

			var info = ImageTools.GetFileInfoFromFile("Images/defaultGroupImage.png", env);

			var image = new GroupImage() {
				Data = ImageTools.GetImageFromFile(info),
				ContentType = info.Extension
			};

			context.GroupImage.Add(image);
			context.SaveChanges();

			Group globalServerGroup = new Group() {
				Name = "Global",
				DateCreated = DateTime.Now,
				IsPrivate = false,
				IsArchived = false,
				Description = "The global chat",
				GroupImageID = image.GroupImageID
			};
			context.Group.Add(globalServerGroup);
			context.SaveChanges();

		}

	}

}

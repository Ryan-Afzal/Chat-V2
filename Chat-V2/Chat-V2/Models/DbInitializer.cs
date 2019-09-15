using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Chat_V2.Models {

	public static class DbInitializer {

		public static void Initialize(ChatContext context) {
			
			context.Database.EnsureCreated();

			if (context.Group.Any()) {
				return;
			}

			Group globalServerGroup = new Group() {
				Name = "Global"
			};

			context.Group.Add(globalServerGroup);
			context.SaveChanges();

		}

	}

}

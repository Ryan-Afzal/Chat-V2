using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Interfaces;
using Chat_V2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chat_V2 {
	public class Program {
		public static void Main(string[] args) {
			var host = CreateWebHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope()) {
				var services = scope.ServiceProvider;
				try {
					var context = services.GetRequiredService<ChatContext>();
					var fileOperationProvider = services.GetRequiredService<IFileOperationProvider>();

					DbInitializer.Initialize(context, fileOperationProvider);
				} catch (Exception ex) {
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred creating the DB.");
				}
			}

			host.Run();
		}

		public static IHostBuilder CreateWebHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => {
					webBuilder.UseStartup<Startup>();
				});

	}
}

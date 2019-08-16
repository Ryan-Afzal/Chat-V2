using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Chat_V2 {
	public class Program {
		public static void Main(string[] args) {
			var host = CreateWebHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope()) {
				var services = scope.ServiceProvider;

				try {
					var serviceProvider = services.GetRequiredService<IServiceProvider>();
					var configuration = services.GetRequiredService<IConfiguration>();
					//CreateRoles(serviceProvider, configuration).Wait();
				} catch (Exception exception) {
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(exception, "An error occurred while creating roles.");
				}
			}

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();

		public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration) {
			var roleManager = serviceProvider.GetRequiredService<RoleManager<ChatRole>>();
			
			string[] roles = {
				"Admin", 
				"Officer", 
				"Moderator", 
				"User"
			};
		}
	}
}

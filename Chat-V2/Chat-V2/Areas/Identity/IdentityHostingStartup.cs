using System;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Chat_V2.Areas.Identity.IdentityHostingStartup))]
namespace Chat_V2.Areas.Identity {
	public class IdentityHostingStartup : IHostingStartup {
		public void Configure(IWebHostBuilder builder) {
			builder.ConfigureServices((context, services) => {
                services.AddDbContext<ChatContext>(options =>

#if DEBUG
                    options.UseSqlServer(
                         context.Configuration.GetConnectionString("ChatContextConnection")
                     ));
#else
                    options.UseNpgsql(
                        @"User ID=postgres;Password=ryanserver083103;Host=localhost;Port=5432;Database=Chat-V2;"
                    ));
#endif

            });
		}
	}
}

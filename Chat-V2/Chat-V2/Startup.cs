using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Hubs;
using Chat_V2.Interfaces;
using Chat_V2.Models;
using Chat_V2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Chat_V2 {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services) {
			services.Configure<CookiePolicyOptions>(options => {
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.Configure<IdentityOptions>(options => {
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;
			});

			services.AddDefaultIdentity<ChatUser>()
				.AddRoles<ChatRole>()
				.AddEntityFrameworkStores<ChatContext>();

			services.Configure<ForwardedHeadersOptions>(options => {
				options.ForwardedHeaders =
					ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			});

			services.AddSignalR(hubOptions => {
				
			}).AddJsonProtocol(options => {
				
			});

			services.AddRazorPages();
			services.AddSingleton<IFileOperationProvider, FileOperationProvider>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseForwardedHeaders();
			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseStaticFiles(new StaticFileOptions {
				FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
				RequestPath = "/Files"
			});

#if DEBUG
			app.UseStaticFiles(new StaticFileOptions {
				FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "DefaultFiles")),
				RequestPath = "/DefaultFiles"
			});
#endif

			app.UseRouting();
			app.UseCors();

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => {
				endpoints.MapHub<NotifHub>("/notifHub");
				endpoints.MapHub<ChatHub>("/chatHub");
				endpoints.MapRazorPages();
			});
		}

	}
}

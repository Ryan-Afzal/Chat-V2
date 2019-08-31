using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Chat_V2.Models.Command;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat_V2.Models {
	public class ChatContext : IdentityDbContext<ChatUser, ChatRole, int> {

		public ChatContext(DbContextOptions<ChatContext> options) : base(options) {
			CommandList = new CommandList();
		}

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}

		public ICommandList CommandList { get; private set; }

		public DbSet<Group> Group { get; set; }
		public DbSet<Membership> Membership { get; set; }
		public DbSet<ChatMessage> ChatMessage { get; set; }

	}
}

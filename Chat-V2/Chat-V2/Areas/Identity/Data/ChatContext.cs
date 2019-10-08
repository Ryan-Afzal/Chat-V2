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

			builder.Entity<MembershipStatus>()
				.Property(e => e.Type)
				.HasConversion<int>();

			builder.Entity<ChatMessage>()
				.Property(e => e.StatusType)
				.HasConversion<int>();
		}

		public ICommandList CommandList { get; private set; }

		public DbSet<Group> Group { get; set; }
		public DbSet<Membership> Membership { get; set; }
		public DbSet<ChatMessage> ChatMessage { get; set; }

		public DbSet<Image> Image { get; set; }

	}
}

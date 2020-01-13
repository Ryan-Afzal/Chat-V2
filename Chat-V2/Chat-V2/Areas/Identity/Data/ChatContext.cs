using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat_V2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat_V2.Models {
	public class ChatContext : IdentityDbContext<ChatUser, ChatRole, int> {

		public ChatContext(DbContextOptions<ChatContext> options) : base(options) {
			
		}

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);
			builder.Entity<MultiuserGroup>()
				.HasBaseType<Group>();
			builder.Entity<PersonalGroup>()
				.HasBaseType<Group>();

			builder.Entity<MultiuserGroupMembership>()
				.HasBaseType<Membership>();
			builder.Entity<PersonalGroupMembership>()
				.HasBaseType<Membership>();
		}

		public DbSet<Group> Group { get; set; }
		public DbSet<Membership> Membership { get; set; }
		public DbSet<ChatMessage> ChatMessage { get; set; }
	}
}

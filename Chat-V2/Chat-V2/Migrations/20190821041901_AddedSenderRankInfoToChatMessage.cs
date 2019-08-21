using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat_V2.Migrations {
	public partial class AddedSenderRankInfoToChatMessage : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AddColumn<int>(
				name: "ChatUserRank",
				table: "ChatMessage",
				nullable: false,
				defaultValue: 0);
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "ChatUserRank",
				table: "ChatMessage");
		}
	}
}

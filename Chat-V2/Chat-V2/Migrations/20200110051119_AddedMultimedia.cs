using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat_V2.Migrations {
	public partial class AddedMultimedia : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AddColumn<string>(
				name: "Multimedia",
				table: "ChatMessage",
				nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "Multimedia",
				table: "ChatMessage");
		}
	}
}

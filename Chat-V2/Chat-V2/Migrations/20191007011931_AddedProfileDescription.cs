using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat_V2.Migrations {
	public partial class AddedProfileDescription : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AddColumn<string>(
				name: "ProfileDescription",
				table: "AspNetUsers",
				nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "ProfileDescription",
				table: "AspNetUsers");
		}
	}
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat_V2.Migrations {
	public partial class AddedRanksToMembership : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AddColumn<int>(
				name: "Rank",
				table: "Membership",
				nullable: false,
				defaultValue: 0);
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "Rank",
				table: "Membership");
		}
	}
}

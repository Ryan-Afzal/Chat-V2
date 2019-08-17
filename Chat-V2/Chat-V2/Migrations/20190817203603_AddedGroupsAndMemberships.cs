using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat_V2.Migrations {
	public partial class AddedGroupsAndMemberships : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Group",
				columns: table => new {
					GroupID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Group", x => x.GroupID);
				});

			migrationBuilder.CreateTable(
				name: "Membership",
				columns: table => new {
					MembershipID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					GroupID = table.Column<int>(nullable: false),
					ChatUserID = table.Column<int>(nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Membership", x => x.MembershipID);
					table.ForeignKey(
						name: "FK_Membership_AspNetUsers_ChatUserID",
						column: x => x.ChatUserID,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Membership_Group_GroupID",
						column: x => x.GroupID,
						principalTable: "Group",
						principalColumn: "GroupID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Membership_ChatUserID",
				table: "Membership",
				column: "ChatUserID");

			migrationBuilder.CreateIndex(
				name: "IX_Membership_GroupID",
				table: "Membership",
				column: "GroupID");
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Membership");

			migrationBuilder.DropTable(
				name: "Group");
		}
	}
}

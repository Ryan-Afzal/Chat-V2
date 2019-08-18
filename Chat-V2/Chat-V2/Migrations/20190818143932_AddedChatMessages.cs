using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat_V2.Migrations {
	public partial class AddedChatMessages : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "ChatMessage",
				columns: table => new {
					ChatMessageID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					GroupID = table.Column<int>(nullable: false),
					ChatUserID = table.Column<int>(nullable: false),
					MinRank = table.Column<int>(nullable: false),
					Message = table.Column<string>(nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_ChatMessage", x => x.ChatMessageID);
					table.ForeignKey(
						name: "FK_ChatMessage_AspNetUsers_ChatUserID",
						column: x => x.ChatUserID,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ChatMessage_Group_GroupID",
						column: x => x.GroupID,
						principalTable: "Group",
						principalColumn: "GroupID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_ChatMessage_ChatUserID",
				table: "ChatMessage",
				column: "ChatUserID");

			migrationBuilder.CreateIndex(
				name: "IX_ChatMessage_GroupID",
				table: "ChatMessage",
				column: "GroupID");
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "ChatMessage");
		}
	}
}

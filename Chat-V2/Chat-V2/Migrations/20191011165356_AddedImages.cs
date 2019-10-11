using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat_V2.Migrations {
	public partial class AddedImages : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AddColumn<int>(
				name: "GroupImageID",
				table: "Group",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "ProfileImageID",
				table: "AspNetUsers",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.CreateTable(
				name: "GroupImage",
				columns: table => new {
					GroupImageID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Data = table.Column<byte[]>(nullable: true),
					ContentType = table.Column<string>(nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_GroupImage", x => x.GroupImageID);
				});

			migrationBuilder.CreateTable(
				name: "ProfileImage",
				columns: table => new {
					ProfileImageID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Data = table.Column<byte[]>(nullable: true),
					ContentType = table.Column<string>(nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_ProfileImage", x => x.ProfileImageID);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Group_GroupImageID",
				table: "Group",
				column: "GroupImageID");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUsers_ProfileImageID",
				table: "AspNetUsers",
				column: "ProfileImageID");

			migrationBuilder.AddForeignKey(
				name: "FK_AspNetUsers_ProfileImage_ProfileImageID",
				table: "AspNetUsers",
				column: "ProfileImageID",
				principalTable: "ProfileImage",
				principalColumn: "ProfileImageID",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Group_GroupImage_GroupImageID",
				table: "Group",
				column: "GroupImageID",
				principalTable: "GroupImage",
				principalColumn: "GroupImageID",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropForeignKey(
				name: "FK_AspNetUsers_ProfileImage_ProfileImageID",
				table: "AspNetUsers");

			migrationBuilder.DropForeignKey(
				name: "FK_Group_GroupImage_GroupImageID",
				table: "Group");

			migrationBuilder.DropTable(
				name: "GroupImage");

			migrationBuilder.DropTable(
				name: "ProfileImage");

			migrationBuilder.DropIndex(
				name: "IX_Group_GroupImageID",
				table: "Group");

			migrationBuilder.DropIndex(
				name: "IX_AspNetUsers_ProfileImageID",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "GroupImageID",
				table: "Group");

			migrationBuilder.DropColumn(
				name: "ProfileImageID",
				table: "AspNetUsers");
		}
	}
}

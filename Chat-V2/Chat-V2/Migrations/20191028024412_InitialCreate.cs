using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Chat_V2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "GroupImage",
                table: "Group",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChatUserName",
                table: "ChatMessage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupImage",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "ChatUserName",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "GroupImageID",
                table: "Group",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageID",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GroupImage",
                columns: table => new
                {
                    GroupImageID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupImage", x => x.GroupImageID);
                });

            migrationBuilder.CreateTable(
                name: "ProfileImage",
                columns: table => new
                {
                    ProfileImageID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
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
    }
}

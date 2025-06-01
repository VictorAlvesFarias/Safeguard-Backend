using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class asdasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64",
                table: "AppFile");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "AppFile");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AppFile");

            migrationBuilder.AddColumn<int>(
                name: "StoredFileId",
                table: "AppFile",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StoredFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MimeType = table.Column<string>(type: "TEXT", nullable: false),
                    Base64 = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoredFile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppFile_StoredFileId",
                table: "AppFile",
                column: "StoredFileId");

            migrationBuilder.CreateIndex(
                name: "IX_StoredFile_UserId",
                table: "StoredFile",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFile_StoredFile_StoredFileId",
                table: "AppFile",
                column: "StoredFileId",
                principalTable: "StoredFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFile_StoredFile_StoredFileId",
                table: "AppFile");

            migrationBuilder.DropTable(
                name: "StoredFile");

            migrationBuilder.DropIndex(
                name: "IX_AppFile_StoredFileId",
                table: "AppFile");

            migrationBuilder.DropColumn(
                name: "StoredFileId",
                table: "AppFile");

            migrationBuilder.AddColumn<string>(
                name: "Base64",
                table: "AppFile",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "AppFile",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AppFile",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}

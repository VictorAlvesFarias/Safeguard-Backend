using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialCreate8780971123123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_Provider_ProviderId",
                table: "Email");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "RecoveryKey");

            migrationBuilder.RenameColumn(
                name: "ReferenceType",
                table: "RecoveryKey",
                newName: "EmailId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Email",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ParentEmailId",
                table: "Email",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Email",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Email_ParentEmailId",
                table: "Email",
                column: "ParentEmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_PlatformId",
                table: "Email",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_Username",
                table: "Email",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Email_Email_ParentEmailId",
                table: "Email",
                column: "ParentEmailId",
                principalTable: "Email",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Email_Platform_PlatformId",
                table: "Email",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Email_Provider_ProviderId",
                table: "Email",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_Email_ParentEmailId",
                table: "Email");

            migrationBuilder.DropForeignKey(
                name: "FK_Email_Platform_PlatformId",
                table: "Email");

            migrationBuilder.DropForeignKey(
                name: "FK_Email_Provider_ProviderId",
                table: "Email");

            migrationBuilder.DropIndex(
                name: "IX_Email_ParentEmailId",
                table: "Email");

            migrationBuilder.DropIndex(
                name: "IX_Email_PlatformId",
                table: "Email");

            migrationBuilder.DropIndex(
                name: "IX_Email_Username",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "ParentEmailId",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Email");

            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "RecoveryKey",
                newName: "ReferenceType");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceId",
                table: "RecoveryKey",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Email",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlatformId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Account_Email_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Email",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Account_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_EmailId",
                table: "Account",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_PlatformId",
                table: "Account",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Email_Provider_ProviderId",
                table: "Email",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

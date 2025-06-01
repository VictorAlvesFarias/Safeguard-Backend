using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class _12321easdasdadsadas87iujads12312321 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_Email_ParentEmailId",
                table: "Email");

            migrationBuilder.DropForeignKey(
                name: "FK_Email_Provider_ProviderId",
                table: "Email");

            migrationBuilder.DropIndex(
                name: "IX_Email_ParentEmailId",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "ParentEmailId",
                table: "Email");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Email",
                newName: "EmailAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Email_ProviderId",
                table: "Email",
                newName: "IX_Email_EmailAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Email_EmailAddress_EmailAddressId",
                table: "Email",
                column: "EmailAddressId",
                principalTable: "EmailAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_EmailAddress_EmailAddressId",
                table: "Email");

            migrationBuilder.RenameColumn(
                name: "EmailAddressId",
                table: "Email",
                newName: "ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Email_EmailAddressId",
                table: "Email",
                newName: "IX_Email_ProviderId");

            migrationBuilder.AddColumn<int>(
                name: "ParentEmailId",
                table: "Email",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Email_ParentEmailId",
                table: "Email",
                column: "ParentEmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Email_Email_ParentEmailId",
                table: "Email",
                column: "ParentEmailId",
                principalTable: "Email",
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
    }
}

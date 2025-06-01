using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class _131231232 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Email_Username",
                table: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Email_Username",
                table: "Email",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Email_Username",
                table: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Email_Username",
                table: "Email",
                column: "Username",
                unique: true);
        }
    }
}

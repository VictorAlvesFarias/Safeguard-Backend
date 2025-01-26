using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "RecoveryKey");

            migrationBuilder.AddColumn<int>(
                name: "EmailId",
                table: "RecoveryEmail",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "RecoveryEmail");

            migrationBuilder.AddColumn<int>(
                name: "EmailId",
                table: "RecoveryKey",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}

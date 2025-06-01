using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class _12321easdasdadsadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "RecoveryEmail");

            migrationBuilder.DropColumn(
                name: "ReferenceType",
                table: "RecoveryEmail");

            migrationBuilder.AddColumn<int>(
                name: "ParentEmailId",
                table: "RecoveryEmail",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentEmailId",
                table: "RecoveryEmail");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceId",
                table: "RecoveryEmail",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceType",
                table: "RecoveryEmail",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}

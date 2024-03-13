using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "NetworkAccount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NetworkAccount_PlatformId",
                table: "NetworkAccount",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_NetworkAccount_Platform_PlatformId",
                table: "NetworkAccount",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NetworkAccount_Platform_PlatformId",
                table: "NetworkAccount");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropIndex(
                name: "IX_NetworkAccount_PlatformId",
                table: "NetworkAccount");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "NetworkAccount");
        }
    }
}

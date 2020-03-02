using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_pageType_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "MenuEntity");

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "MenuEntity",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Row",
                table: "MenuEntity");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "MenuEntity",
                nullable: false,
                defaultValue: 0);
        }
    }
}

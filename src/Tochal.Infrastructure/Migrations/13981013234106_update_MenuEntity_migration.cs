using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_MenuEntity_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "MenuEntity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "MenuEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageType",
                table: "MenuEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShortContent",
                table: "MenuEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "MenuEntity",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "MenuEntity");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "MenuEntity");

            migrationBuilder.DropColumn(
                name: "PageType",
                table: "MenuEntity");

            migrationBuilder.DropColumn(
                name: "ShortContent",
                table: "MenuEntity");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "MenuEntity");
        }
    }
}

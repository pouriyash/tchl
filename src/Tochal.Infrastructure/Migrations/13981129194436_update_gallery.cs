using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_gallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileImage",
                table: "MenuEntity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GalleryType",
                table: "Gallery",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileImage",
                table: "MenuEntity");

            migrationBuilder.DropColumn(
                name: "GalleryType",
                table: "Gallery");
        }
    }
}

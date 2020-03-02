using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_Gallery_firstImage_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstImageGalleryId",
                table: "Gallery");

            migrationBuilder.AddColumn<bool>(
                name: "FirstImage",
                table: "Gallery",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstImage",
                table: "Gallery");

            migrationBuilder.AddColumn<int>(
                name: "FirstImageGalleryId",
                table: "Gallery",
                nullable: true);
        }
    }
}

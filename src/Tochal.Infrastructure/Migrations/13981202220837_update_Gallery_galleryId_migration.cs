using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_Gallery_galleryId_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupGalleryId",
                table: "Gallery",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupGalleryId",
                table: "Gallery");
        }
    }
}

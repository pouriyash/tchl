using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class uppdate_GalleryEntity_galleries_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Gallery_GroupGalleryId",
                table: "Gallery",
                column: "GroupGalleryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_GroupGallery_GroupGalleryId",
                table: "Gallery",
                column: "GroupGalleryId",
                principalTable: "GroupGallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_GroupGallery_GroupGalleryId",
                table: "Gallery");

            migrationBuilder.DropIndex(
                name: "IX_Gallery_GroupGalleryId",
                table: "Gallery");
        }
    }
}

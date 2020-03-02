using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_Gallery_gallery_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirstImageId",
                table: "GroupGallery",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GalleryEntityId",
                table: "GroupGallery",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupGallery_FirstImageId",
                table: "GroupGallery",
                column: "FirstImageId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupGallery_GalleryEntityId",
                table: "GroupGallery",
                column: "GalleryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupGallery_Gallery_FirstImageId",
                table: "GroupGallery",
                column: "FirstImageId",
                principalTable: "Gallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupGallery_GalleryEntity_GalleryEntityId",
                table: "GroupGallery",
                column: "GalleryEntityId",
                principalTable: "GalleryEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupGallery_Gallery_FirstImageId",
                table: "GroupGallery");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupGallery_GalleryEntity_GalleryEntityId",
                table: "GroupGallery");

            migrationBuilder.DropIndex(
                name: "IX_GroupGallery_FirstImageId",
                table: "GroupGallery");

            migrationBuilder.DropIndex(
                name: "IX_GroupGallery_GalleryEntityId",
                table: "GroupGallery");

            migrationBuilder.DropColumn(
                name: "FirstImageId",
                table: "GroupGallery");

            migrationBuilder.DropColumn(
                name: "GalleryEntityId",
                table: "GroupGallery");
        }
    }
}

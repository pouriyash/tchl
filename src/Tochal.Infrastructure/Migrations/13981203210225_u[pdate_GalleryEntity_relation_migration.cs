using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_GalleryEntity_relation_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupGallery_GalleryEntity_GalleryEntityId",
                table: "GroupGallery");

            migrationBuilder.DropIndex(
                name: "IX_GroupGallery_GalleryEntityId",
                table: "GroupGallery");

            migrationBuilder.DropColumn(
                name: "GalleryEntityId",
                table: "GroupGallery");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryEntity_galleryGroupId",
                table: "GalleryEntity",
                column: "galleryGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryEntity_GroupGallery_galleryGroupId",
                table: "GalleryEntity",
                column: "galleryGroupId",
                principalTable: "GroupGallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryEntity_GroupGallery_galleryGroupId",
                table: "GalleryEntity");

            migrationBuilder.DropIndex(
                name: "IX_GalleryEntity_galleryGroupId",
                table: "GalleryEntity");

            migrationBuilder.AddColumn<int>(
                name: "GalleryEntityId",
                table: "GroupGallery",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupGallery_GalleryEntityId",
                table: "GroupGallery",
                column: "GalleryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupGallery_GalleryEntity_GalleryEntityId",
                table: "GroupGallery",
                column: "GalleryEntityId",
                principalTable: "GalleryEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

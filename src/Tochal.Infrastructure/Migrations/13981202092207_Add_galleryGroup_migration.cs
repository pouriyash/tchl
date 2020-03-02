using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class Add_galleryGroup_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GalleryType",
                table: "Gallery");

            migrationBuilder.AddColumn<int>(
                name: "FirstImageGalleryId",
                table: "Gallery",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GalleryEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    galleryGroupId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryGroup", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryEntity");

            migrationBuilder.DropTable(
                name: "GalleryGroup");

            migrationBuilder.DropColumn(
                name: "FirstImageGalleryId",
                table: "Gallery");

            migrationBuilder.AddColumn<int>(
                name: "GalleryType",
                table: "Gallery",
                nullable: false,
                defaultValue: 0);
        }
    }
}

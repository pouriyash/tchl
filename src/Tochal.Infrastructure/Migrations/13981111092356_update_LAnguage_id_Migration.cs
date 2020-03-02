using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_LAnguage_id_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    LanguageTitle = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentEntity_Lang_Id",
                table: "ContentEntity",
                column: "Lang_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentEntity_Language_Lang_Id",
                table: "ContentEntity",
                column: "Lang_Id",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentEntity_Language_Lang_Id",
                table: "ContentEntity");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropIndex(
                name: "IX_ContentEntity_Lang_Id",
                table: "ContentEntity");
        }
    }
}

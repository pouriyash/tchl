using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_menu_langId_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Lang_Id",
                table: "MenuEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuEntity_Lang_Id",
                table: "MenuEntity",
                column: "Lang_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuEntity_Language_Lang_Id",
                table: "MenuEntity",
                column: "Lang_Id",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuEntity_Language_Lang_Id",
                table: "MenuEntity");

            migrationBuilder.DropIndex(
                name: "IX_MenuEntity_Lang_Id",
                table: "MenuEntity");

            migrationBuilder.DropColumn(
                name: "Lang_Id",
                table: "MenuEntity");
        }
    }
}

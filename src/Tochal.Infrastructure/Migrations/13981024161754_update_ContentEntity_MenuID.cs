using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_ContentEntity_MenuID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuTitleId",
                table: "ContentEntity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubMenuId",
                table: "ContentEntity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentEntity_MenuTitleId",
                table: "ContentEntity",
                column: "MenuTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentEntity_SubMenuId",
                table: "ContentEntity",
                column: "SubMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentEntity_MenuEntity_MenuTitleId",
                table: "ContentEntity",
                column: "MenuTitleId",
                principalTable: "MenuEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentEntity_MenuEntity_SubMenuId",
                table: "ContentEntity",
                column: "SubMenuId",
                principalTable: "MenuEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentEntity_MenuEntity_MenuTitleId",
                table: "ContentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentEntity_MenuEntity_SubMenuId",
                table: "ContentEntity");

            migrationBuilder.DropIndex(
                name: "IX_ContentEntity_MenuTitleId",
                table: "ContentEntity");

            migrationBuilder.DropIndex(
                name: "IX_ContentEntity_SubMenuId",
                table: "ContentEntity");

            migrationBuilder.DropColumn(
                name: "MenuTitleId",
                table: "ContentEntity");

            migrationBuilder.DropColumn(
                name: "SubMenuId",
                table: "ContentEntity");
        }
    }
}

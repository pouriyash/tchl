using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_contentEntity_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainVideo",
                table: "ContentEntity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "ContentEntity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Showlocation",
                table: "ContentEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContentEntity_MenuId",
                table: "ContentEntity",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentEntity_MenuEntity_MenuId",
                table: "ContentEntity",
                column: "MenuId",
                principalTable: "MenuEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentEntity_MenuEntity_MenuId",
                table: "ContentEntity");

            migrationBuilder.DropIndex(
                name: "IX_ContentEntity_MenuId",
                table: "ContentEntity");

            migrationBuilder.DropColumn(
                name: "MainVideo",
                table: "ContentEntity");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "ContentEntity");

            migrationBuilder.DropColumn(
                name: "Showlocation",
                table: "ContentEntity");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_MenuEntity_lang_Id_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuEntity_Language_Lang_Id",
                table: "MenuEntity");

            migrationBuilder.AlterColumn<int>(
                name: "Lang_Id",
                table: "MenuEntity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MenuEntity_Language_Lang_Id",
                table: "MenuEntity",
                column: "Lang_Id",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuEntity_Language_Lang_Id",
                table: "MenuEntity");

            migrationBuilder.AlterColumn<int>(
                name: "Lang_Id",
                table: "MenuEntity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuEntity_Language_Lang_Id",
                table: "MenuEntity",
                column: "Lang_Id",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

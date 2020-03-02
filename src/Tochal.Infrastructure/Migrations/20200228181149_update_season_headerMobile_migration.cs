using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_season_headerMobile_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AutumnHeaderMobileImage",
                table: "Season",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FallHeaderMobileImage",
                table: "Season",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpringHeaderMobileImage",
                table: "Season",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WinterHeaderMobileImage",
                table: "Season",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutumnHeaderMobileImage",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "FallHeaderMobileImage",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "SpringHeaderMobileImage",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "WinterHeaderMobileImage",
                table: "Season");
        }
    }
}

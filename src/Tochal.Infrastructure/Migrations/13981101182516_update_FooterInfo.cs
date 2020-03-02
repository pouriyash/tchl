using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_FooterInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FooterIcon");

            migrationBuilder.AddColumn<string>(
                name: "InstagramLink",
                table: "FooterInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PintrestLink",
                table: "FooterInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelegramLink",
                table: "FooterInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsAppLink",
                table: "FooterInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstagramLink",
                table: "FooterInfo");

            migrationBuilder.DropColumn(
                name: "PintrestLink",
                table: "FooterInfo");

            migrationBuilder.DropColumn(
                name: "TelegramLink",
                table: "FooterInfo");

            migrationBuilder.DropColumn(
                name: "WhatsAppLink",
                table: "FooterInfo");

            migrationBuilder.CreateTable(
                name: "FooterIcon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FooterId = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterIcon", x => x.Id);
                });
        }
    }
}

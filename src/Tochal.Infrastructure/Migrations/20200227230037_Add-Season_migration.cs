using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class AddSeason_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SpringTitle = table.Column<string>(nullable: true),
                    SpringShortDescription = table.Column<string>(nullable: true),
                    SpringContent = table.Column<string>(nullable: true),
                    SpringHeaderImage = table.Column<string>(nullable: true),
                    WinterTitle = table.Column<string>(nullable: true),
                    WinterShortDescription = table.Column<string>(nullable: true),
                    WinterContent = table.Column<string>(nullable: true),
                    WinterHeaderImage = table.Column<string>(nullable: true),
                    AutumnTitle = table.Column<string>(nullable: true),
                    AutumnShortDescription = table.Column<string>(nullable: true),
                    AutumnContent = table.Column<string>(nullable: true),
                    AutumnHeaderImage = table.Column<string>(nullable: true),
                    FallTitle = table.Column<string>(nullable: true),
                    FallShortDescription = table.Column<string>(nullable: true),
                    FallContent = table.Column<string>(nullable: true),
                    FallHeaderImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Season");
        }
    }
}

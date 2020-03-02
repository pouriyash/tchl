using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class Add_BlogContentEntity_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogContentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 300, nullable: true),
                    SubTitle = table.Column<string>(maxLength: 300, nullable: true),
                    ShortContent = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ThumbnailImage = table.Column<string>(nullable: true),
                    MainImage = table.Column<string>(nullable: true),
                    ExternalLink = table.Column<string>(maxLength: 500, nullable: true),
                    BlogContentEntityType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogContentEntity", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogContentEntity");
        }
    }
}

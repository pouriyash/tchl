using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class remove_name_contactus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ContactUs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ContactUs",
                nullable: true);
        }
    }
}

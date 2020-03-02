using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_name_summer_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FallTitle",
                table: "Season",
                newName: "SummerTitle");

            migrationBuilder.RenameColumn(
                name: "FallShortDescription",
                table: "Season",
                newName: "SummerShortDescription");

            migrationBuilder.RenameColumn(
                name: "FallHeaderMobileImage",
                table: "Season",
                newName: "SummerHeaderMobileImage");

            migrationBuilder.RenameColumn(
                name: "FallHeaderImage",
                table: "Season",
                newName: "SummerHeaderImage");

            migrationBuilder.RenameColumn(
                name: "FallContent",
                table: "Season",
                newName: "SummerContent");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Notification",
                newName: "Body");

            migrationBuilder.RenameColumn(
                name: "ShowDate",
                table: "Notification",
                newName: "ScheduleDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Notification",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Notification",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastRetryDate",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Module",
                table: "Notification",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Notification",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "Notification",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubSystemId",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubSystemType",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notification",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToUserId",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Notification",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    CreateAnswerUserId = table.Column<int>(nullable: false),
                    UpdateAnswerUserId = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    IsAnswer = table.Column<bool>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    userId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateSend = table.Column<DateTime>(nullable: true),
                    DateAnswer = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "LastRetryDate",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Module",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "SubSystemId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "SubSystemType",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ToUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "SummerTitle",
                table: "Season",
                newName: "FallTitle");

            migrationBuilder.RenameColumn(
                name: "SummerShortDescription",
                table: "Season",
                newName: "FallShortDescription");

            migrationBuilder.RenameColumn(
                name: "SummerHeaderMobileImage",
                table: "Season",
                newName: "FallHeaderMobileImage");

            migrationBuilder.RenameColumn(
                name: "SummerHeaderImage",
                table: "Season",
                newName: "FallHeaderImage");

            migrationBuilder.RenameColumn(
                name: "SummerContent",
                table: "Season",
                newName: "FallContent");

            migrationBuilder.RenameColumn(
                name: "ScheduleDate",
                table: "Notification",
                newName: "ShowDate");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Notification",
                newName: "Text");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class upadte_contactUs_name_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "CreateAnswerUserId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "DateAnswer",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "DateSend",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "IsAnswer",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "UpdateAnswerUserId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "ContactUs");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ContactUs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "ContactUs",
                newName: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ContactUs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "ContactUs",
                newName: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateAnswerUserId",
                table: "ContactUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAnswer",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSend",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnswer",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateAnswerUserId",
                table: "ContactUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "ContactUs",
                nullable: true);
        }
    }
}

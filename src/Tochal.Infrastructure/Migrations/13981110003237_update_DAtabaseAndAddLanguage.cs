using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_DAtabaseAndAddLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_BankCities_CityId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_BankProvinces_ProvinceId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_BankUniversityType_UniTypeId",
                table: "AppUsers");

            migrationBuilder.DropTable(
                name: "BankCities");

            migrationBuilder.DropTable(
                name: "BankUniversityType");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "BankProvinces");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_CityId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_ProvinceId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_UniTypeId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ActiveIntershipNumber",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CenterType",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CompanyConfirm",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CompanyNameEn",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CompanyNationalId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CompanyNationalMainId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CompanyNationalMainName",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "EconomicalCode",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ExpertTochal",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "IndustryRelationsManagerEmail",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "IndustryRelationsManagerName",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "IsAcceptCompany",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "IsAcceptUniversity",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "IsIrainian",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ManagingDirectorName",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "RelatedOn",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UniManagerName",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UniTypeId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UniversityManagerName",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UniversityName",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "industrialType",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "registrationNumber",
                table: "AppUsers");

            migrationBuilder.AddColumn<int>(
                name: "Lang_Id",
                table: "ContentEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageTitle = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentEntity_Lang_Id",
                table: "ContentEntity",
                column: "Lang_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentEntity_Language_Lang_Id",
                table: "ContentEntity",
                column: "Lang_Id",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentEntity_Language_Lang_Id",
                table: "ContentEntity");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropIndex(
                name: "IX_ContentEntity_Lang_Id",
                table: "ContentEntity");

            migrationBuilder.DropColumn(
                name: "Lang_Id",
                table: "ContentEntity");

            migrationBuilder.AddColumn<int>(
                name: "ActiveIntershipNumber",
                table: "AppUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CenterType",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyConfirm",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AppUsers",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyNameEn",
                table: "AppUsers",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyNationalId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyNationalMainId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyNationalMainName",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EconomicalCode",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpertTochal",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustryRelationsManagerEmail",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustryRelationsManagerName",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcceptCompany",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcceptUniversity",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIrainian",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagingDirectorName",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelatedOn",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniManagerName",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniTypeId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniversityManagerName",
                table: "AppUsers",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniversityName",
                table: "AppUsers",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "AppUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WebSite",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "industrialType",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "registrationNumber",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankProvinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProvinceName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankProvinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankUniversityType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankUniversityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoutineFlownDate = table.Column<DateTime>(nullable: true),
                    RoutineIsDone = table.Column<bool>(nullable: false),
                    RoutineIsFlown = table.Column<bool>(nullable: false),
                    RoutineIsSucceeded = table.Column<bool>(nullable: false),
                    RoutineStep = table.Column<int>(nullable: false),
                    RoutineStepChangeDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankCities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCities_BankProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "BankProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_CityId",
                table: "AppUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ProvinceId",
                table: "AppUsers",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_UniTypeId",
                table: "AppUsers",
                column: "UniTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCities_ProvinceId",
                table: "BankCities",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_BankCities_CityId",
                table: "AppUsers",
                column: "CityId",
                principalTable: "BankCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_BankProvinces_ProvinceId",
                table: "AppUsers",
                column: "ProvinceId",
                principalTable: "BankProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_BankUniversityType_UniTypeId",
                table: "AppUsers",
                column: "UniTypeId",
                principalTable: "BankUniversityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

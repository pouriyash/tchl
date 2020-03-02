using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochal.Infrastructure.Migrations
{
    public partial class update_DataBase_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankEducationalOrientation");

            migrationBuilder.DropTable(
                name: "EvaluationAnswers");

            migrationBuilder.DropTable(
                name: "Ministry");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "BankEducationalField");

            migrationBuilder.DropTable(
                name: "EvaluationQuestion");

            migrationBuilder.DropTable(
                name: "BankEducationalGrade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankEducationalGrade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankEducationalGrade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Answers = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ministry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ministry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankMessage = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    DatePayment = table.Column<DateTime>(nullable: false),
                    DateTimePaid = table.Column<DateTime>(nullable: true),
                    EntityId = table.Column<int>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductType = table.Column<string>(nullable: true),
                    RetrivalRefNo = table.Column<long>(nullable: false),
                    SystemTraceNo = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    TransactionReferenceId = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankEducationalField",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankEducationalGradeId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankEducationalField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankEducationalField_BankEducationalGrade_BankEducationalGradeId",
                        column: x => x.BankEducationalGradeId,
                        principalTable: "BankEducationalGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    TochalId = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationAnswers_EvaluationQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "EvaluationQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankEducationalOrientation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankEducationalFieldId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankEducationalOrientation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankEducationalOrientation_BankEducationalField_BankEducationalFieldId",
                        column: x => x.BankEducationalFieldId,
                        principalTable: "BankEducationalField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankEducationalField_BankEducationalGradeId",
                table: "BankEducationalField",
                column: "BankEducationalGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankEducationalOrientation_BankEducationalFieldId",
                table: "BankEducationalOrientation",
                column: "BankEducationalFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationAnswers_QuestionId",
                table: "EvaluationAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
                column: "UserId");
        }
    }
}

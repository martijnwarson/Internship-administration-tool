using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class FeedbackAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Remarks");

            migrationBuilder.AddColumn<long>(
                name: "FeedBackId",
                table: "Validations",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FeedBackId",
                table: "Internships",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FeedBackId",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeedBacks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBacks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Validations_FeedBackId",
                table: "Validations",
                column: "FeedBackId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_FeedBackId",
                table: "Internships",
                column: "FeedBackId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_FeedBackId",
                table: "Companies",
                column: "FeedBackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_FeedBacks_FeedBackId",
                table: "Companies",
                column: "FeedBackId",
                principalTable: "FeedBacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_FeedBacks_FeedBackId",
                table: "Internships",
                column: "FeedBackId",
                principalTable: "FeedBacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                table: "Persons",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Validations_FeedBacks_FeedBackId",
                table: "Validations",
                column: "FeedBackId",
                principalTable: "FeedBacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_FeedBacks_FeedBackId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Internships_FeedBacks_FeedBackId",
                table: "Internships");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Validations_FeedBacks_FeedBackId",
                table: "Validations");

            migrationBuilder.DropTable(
                name: "FeedBacks");

            migrationBuilder.DropIndex(
                name: "IX_Validations_FeedBackId",
                table: "Validations");

            migrationBuilder.DropIndex(
                name: "IX_Internships_FeedBackId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Companies_FeedBackId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FeedBackId",
                table: "Validations");

            migrationBuilder.DropColumn(
                name: "FeedBackId",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "FeedBackId",
                table: "Companies");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidationId = table.Column<long>(type: "bigint", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Validations_ValidationId",
                        column: x => x.ValidationId,
                        principalTable: "Validations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Remarks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidationId = table.Column<long>(type: "bigint", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remarks_Validations_ValidationId",
                        column: x => x.ValidationId,
                        principalTable: "Validations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ValidationId",
                table: "Questions",
                column: "ValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_ValidationId",
                table: "Remarks",
                column: "ValidationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                table: "Persons",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class adapted_StringEntity_to_Questions_Remarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Periods_Internships_InternshipId",
                table: "Periods");

            migrationBuilder.DropTable(
                name: "StringEntities");

            migrationBuilder.DropIndex(
                name: "IX_Periods_InternshipId",
                table: "Periods");

            migrationBuilder.DropColumn(
                name: "InternshipId",
                table: "Periods");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false),
                    ValidationId = table.Column<long>(nullable: true)
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false),
                    ValidationId = table.Column<long>(nullable: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Remarks");

            migrationBuilder.AddColumn<long>(
                name: "InternshipId",
                table: "Periods",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StringEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidationId = table.Column<long>(type: "bigint", nullable: true),
                    ValidationId1 = table.Column<long>(type: "bigint", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StringEntities_Validations_ValidationId",
                        column: x => x.ValidationId,
                        principalTable: "Validations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StringEntities_Validations_ValidationId1",
                        column: x => x.ValidationId1,
                        principalTable: "Validations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Periods_InternshipId",
                table: "Periods",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_StringEntities_ValidationId",
                table: "StringEntities",
                column: "ValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_StringEntities_ValidationId1",
                table: "StringEntities",
                column: "ValidationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Periods_Internships_InternshipId",
                table: "Periods",
                column: "InternshipId",
                principalTable: "Internships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

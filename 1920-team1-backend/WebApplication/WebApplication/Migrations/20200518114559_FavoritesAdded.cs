using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class FavoritesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internships_Persons_StudentId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Internships_StudentId",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Internships");

            migrationBuilder.CreateTable(
                name: "StudentInternShip",
                columns: table => new
                {
                    InternshipId = table.Column<long>(nullable: false),
                    StudentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInternShip", x => new { x.StudentId, x.InternshipId });
                    table.ForeignKey(
                        name: "FK_StudentInternShip_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentInternShip_Persons_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentInternShip_InternshipId",
                table: "StudentInternShip",
                column: "InternshipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentInternShip");

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Internships",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Internships_StudentId",
                table: "Internships",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_Persons_StudentId",
                table: "Internships",
                column: "StudentId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

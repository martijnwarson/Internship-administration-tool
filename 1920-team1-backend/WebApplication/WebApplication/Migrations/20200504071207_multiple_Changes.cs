using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class multiple_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Persons",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Periods",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NrOfSupportEmployees",
                table: "Internships",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Periods");

            migrationBuilder.DropColumn(
                name: "NrOfSupportEmployees",
                table: "Internships");
        }
    }
}

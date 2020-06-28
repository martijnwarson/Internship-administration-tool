using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class initial_create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(nullable: false),
                    Number = table.Column<string>(nullable: false),
                    Box = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    CourseId = table.Column<long>(nullable: true),
                    AddressId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    AmountOfEmployees = table.Column<int>(nullable: false),
                    AmountOfEmployeesIt = table.Column<int>(nullable: false),
                    AddressId = table.Column<long>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    ContactPersonId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Persons_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LectorCourse",
                columns: table => new
                {
                    LectorId = table.Column<long>(nullable: false),
                    CourseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectorCourse", x => new { x.LectorId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_LectorCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LectorCourse_Persons_LectorId",
                        column: x => x.LectorId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Internships",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ContactPersonId = table.Column<long>(nullable: false),
                    AddressId = table.Column<long>(nullable: false),
                    TechDescription = table.Column<string>(nullable: false),
                    ResearchTopic = table.Column<string>(nullable: false),
                    Application = table.Column<bool>(nullable: false),
                    Résumée = table.Column<bool>(nullable: false),
                    Reimbursement = table.Column<bool>(nullable: false),
                    StudentAmount = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: false),
                    StudentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Internships_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Internships_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Internships_Persons_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Internships_Persons_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "InternshipCourse",
                columns: table => new
                {
                    InternshipId = table.Column<long>(nullable: false),
                    CourseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipCourse", x => new { x.InternshipId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_InternshipCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InternshipCourse_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "InternshipPerson",
                columns: table => new
                {
                    InternshipId = table.Column<long>(nullable: false),
                    PersonId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipPerson", x => new { x.InternshipId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_InternshipPerson_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InternshipPerson_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "InternshipStudent",
                columns: table => new
                {
                    InternshipId = table.Column<long>(nullable: false),
                    StudentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipStudent", x => new { x.InternshipId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_InternshipStudent_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InternshipStudent_Persons_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "InternshipTechnology",
                columns: table => new
                {
                    InternshipId = table.Column<long>(nullable: false),
                    TechnologyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipTechnology", x => new { x.InternshipId, x.TechnologyId });
                    table.ForeignKey(
                        name: "FK_InternshipTechnology_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InternshipTechnology_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    InternshipId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Periods_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Validations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternshipId = table.Column<long>(nullable: false),
                    LectorId = table.Column<long>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Validations_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Validations_Persons_LectorId",
                        column: x => x.LectorId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "InternshipPeriod",
                columns: table => new
                {
                    InternshipId = table.Column<long>(nullable: false),
                    PeriodId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipPeriod", x => new { x.InternshipId, x.PeriodId });
                    table.ForeignKey(
                        name: "FK_InternshipPeriod_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InternshipPeriod_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StringEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false),
                    ValidationId = table.Column<long>(nullable: true),
                    ValidationId1 = table.Column<long>(nullable: true)
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
                name: "IX_Companies_AddressId",
                table: "Companies",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ContactPersonId",
                table: "Companies",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipCourse_CourseId",
                table: "InternshipCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPeriod_PeriodId",
                table: "InternshipPeriod",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPerson_PersonId",
                table: "InternshipPerson",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_AddressId",
                table: "Internships",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_CompanyId",
                table: "Internships",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_ContactPersonId",
                table: "Internships",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_StudentId",
                table: "Internships",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipStudent_StudentId",
                table: "InternshipStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipTechnology_TechnologyId",
                table: "InternshipTechnology",
                column: "TechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_LectorCourse_CourseId",
                table: "LectorCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Periods_InternshipId",
                table: "Periods",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AddressId",
                table: "Persons",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CourseId",
                table: "Persons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StringEntities_ValidationId",
                table: "StringEntities",
                column: "ValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_StringEntities_ValidationId1",
                table: "StringEntities",
                column: "ValidationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Validations_InternshipId",
                table: "Validations",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Validations_LectorId",
                table: "Validations",
                column: "LectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternshipCourse");

            migrationBuilder.DropTable(
                name: "InternshipPeriod");

            migrationBuilder.DropTable(
                name: "InternshipPerson");

            migrationBuilder.DropTable(
                name: "InternshipStudent");

            migrationBuilder.DropTable(
                name: "InternshipTechnology");

            migrationBuilder.DropTable(
                name: "LectorCourse");

            migrationBuilder.DropTable(
                name: "StringEntities");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "Validations");

            migrationBuilder.DropTable(
                name: "Internships");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}

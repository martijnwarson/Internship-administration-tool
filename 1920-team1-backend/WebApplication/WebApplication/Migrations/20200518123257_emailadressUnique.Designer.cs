﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication.Data;

namespace WebApplication.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200518123257_emailadressUnique")]
    partial class emailadressUnique
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication.Models.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Box")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("WebApplication.Models.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AddressId")
                        .HasColumnType("bigint");

                    b.Property<int>("AmountOfEmployees")
                        .HasColumnType("int");

                    b.Property<int>("AmountOfEmployeesIt")
                        .HasColumnType("int");

                    b.Property<long>("ContactPersonId")
                        .HasColumnType("bigint");

                    b.Property<long?>("FeedBackId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReasonOfInactive")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ContactPersonId");

                    b.HasIndex("FeedBackId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("WebApplication.Models.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("WebApplication.Models.FeedBack", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FeedBacks");
                });

            modelBuilder.Entity("WebApplication.Models.Internship", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AddressId")
                        .HasColumnType("bigint");

                    b.Property<bool?>("Application")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Conditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ContactPersonId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("FeedBackId")
                        .HasColumnType("bigint");

                    b.Property<int>("NrOfSupportEmployees")
                        .HasColumnType("int");

                    b.Property<bool?>("Reimbursement")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResearchTopic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Résumée")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentAmount")
                        .HasColumnType("int");

                    b.Property<string>("TechDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ContactPersonId");

                    b.HasIndex("FeedBackId");

                    b.ToTable("Internships");
                });

            modelBuilder.Entity("WebApplication.Models.InternshipCourse", b =>
                {
                    b.Property<long>("InternshipId")
                        .HasColumnType("bigint");

                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.HasKey("InternshipId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("InternshipCourse");
                });

            modelBuilder.Entity("WebApplication.Models.InternshipPeriod", b =>
                {
                    b.Property<long>("InternshipId")
                        .HasColumnType("bigint");

                    b.Property<long>("PeriodId")
                        .HasColumnType("bigint");

                    b.HasKey("InternshipId", "PeriodId");

                    b.HasIndex("PeriodId");

                    b.ToTable("InternshipPeriod");
                });

            modelBuilder.Entity("WebApplication.Models.InternshipPerson", b =>
                {
                    b.Property<long>("InternshipId")
                        .HasColumnType("bigint");

                    b.Property<long>("PersonId")
                        .HasColumnType("bigint");

                    b.HasKey("InternshipId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("InternshipPerson");
                });

            modelBuilder.Entity("WebApplication.Models.InternshipStudent", b =>
                {
                    b.Property<long>("InternshipId")
                        .HasColumnType("bigint");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.HasKey("InternshipId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("InternshipStudent");
                });

            modelBuilder.Entity("WebApplication.Models.InternshipTechnology", b =>
                {
                    b.Property<long>("InternshipId")
                        .HasColumnType("bigint");

                    b.Property<long>("TechnologyId")
                        .HasColumnType("bigint");

                    b.HasKey("InternshipId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("InternshipTechnology");
                });

            modelBuilder.Entity("WebApplication.Models.LectorCourse", b =>
                {
                    b.Property<long>("LectorId")
                        .HasColumnType("bigint");

                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.HasKey("LectorId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("LectorCourse");
                });

            modelBuilder.Entity("WebApplication.Models.Period", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("WebApplication.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("WebApplication.Models.StudentInternShip", b =>
                {
                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<long>("InternshipId")
                        .HasColumnType("bigint");

                    b.HasKey("StudentId", "InternshipId");

                    b.HasIndex("InternshipId");

                    b.ToTable("StudentInternShip");
                });

            modelBuilder.Entity("WebApplication.Models.Technology", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("WebApplication.Models.Validation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("FeedBackId")
                        .HasColumnType("bigint");

                    b.Property<long>("InternshipId")
                        .HasColumnType("bigint");

                    b.Property<long>("LectorId")
                        .HasColumnType("bigint");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FeedBackId");

                    b.HasIndex("InternshipId");

                    b.HasIndex("LectorId");

                    b.ToTable("Validations");
                });

            modelBuilder.Entity("WebApplication.Models.Lector", b =>
                {
                    b.HasBaseType("WebApplication.Models.Person");

                    b.HasDiscriminator().HasValue("Lector");
                });

            modelBuilder.Entity("WebApplication.Models.Student", b =>
                {
                    b.HasBaseType("WebApplication.Models.Person");

                    b.Property<long?>("AddressId")
                        .HasColumnType("bigint");

                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.HasIndex("AddressId");

                    b.HasIndex("CourseId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("WebApplication.Models.Coördinator", b =>
                {
                    b.HasBaseType("WebApplication.Models.Lector");

                    b.HasDiscriminator().HasValue("Coördinator");
                });

            modelBuilder.Entity("WebApplication.Models.Company", b =>
                {
                    b.HasOne("WebApplication.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Person", "ContactPerson")
                        .WithMany()
                        .HasForeignKey("ContactPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.FeedBack", "FeedBack")
                        .WithMany()
                        .HasForeignKey("FeedBackId");
                });

            modelBuilder.Entity("WebApplication.Models.Internship", b =>
                {
                    b.HasOne("WebApplication.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Company", "Company")
                        .WithMany("Internships")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Person", "ContactPerson")
                        .WithMany()
                        .HasForeignKey("ContactPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.FeedBack", "FeedBack")
                        .WithMany()
                        .HasForeignKey("FeedBackId");
                });

            modelBuilder.Entity("WebApplication.Models.InternshipCourse", b =>
                {
                    b.HasOne("WebApplication.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Internship", "Internship")
                        .WithMany("Courses")
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication.Models.InternshipPeriod", b =>
                {
                    b.HasOne("WebApplication.Models.Internship", "Internship")
                        .WithMany("Periods")
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Period", "Period")
                        .WithMany()
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication.Models.InternshipPerson", b =>
                {
                    b.HasOne("WebApplication.Models.Internship", "Internship")
                        .WithMany("Promotors")
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication.Models.InternshipStudent", b =>
                {
                    b.HasOne("WebApplication.Models.Internship", "Internship")
                        .WithMany("Students")
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication.Models.InternshipTechnology", b =>
                {
                    b.HasOne("WebApplication.Models.Internship", "Internship")
                        .WithMany("Technologies")
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication.Models.LectorCourse", b =>
                {
                    b.HasOne("WebApplication.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Lector", "Lector")
                        .WithMany("Courses")
                        .HasForeignKey("LectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication.Models.StudentInternShip", b =>
                {
                    b.HasOne("WebApplication.Models.Internship", "Internship")
                        .WithMany()
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Student", "Student")
                        .WithMany("Favorites")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication.Models.Validation", b =>
                {
                    b.HasOne("WebApplication.Models.FeedBack", "FeedBack")
                        .WithMany()
                        .HasForeignKey("FeedBackId");

                    b.HasOne("WebApplication.Models.Internship", "Internship")
                        .WithMany("Validations")
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Models.Lector", "Lector")
                        .WithMany()
                        .HasForeignKey("LectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication.Models.Student", b =>
                {
                    b.HasOne("WebApplication.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("WebApplication.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

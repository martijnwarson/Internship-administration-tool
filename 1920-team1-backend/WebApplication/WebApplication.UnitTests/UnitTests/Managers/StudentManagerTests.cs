using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using WebApplication.EmailService;
using WebApplication.Enums;
using WebApplication.Helpers;
using WebApplication.Managers;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.UnitTests.UnitTests.InMemoryRepositories;

namespace WebApplication.UnitTests.UnitTests.Managers
{
    public class StudentManagerTests : SutTests<StudentManager>
    {
        private readonly IList<Student> _students = new List<Student>();
        private readonly IList<Course> _courses = new List<Course>();
        private readonly IList<Internship> _internships = new List<Internship>();
        private Student _studentBauknecht;
        private Course _courseApplicatie;
        private Internship _internship;

        public StudentManagerTests()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            IPasswordGenerator passwordGenerator = new PasswordGenerator(configuration);
            ICourseManager courseManager = new CourseManager(new InMemoryCourseRepository(_courses));

            ISendGridService sendGridService = new SendGridService();
            IPdfGenerator pdfGenerator = new PdfGenerator();
            IEmailManager emailManager = new EmailManager(sendGridService, pdfGenerator);
            Sut = new StudentManager(
                new InMemoryStudentRepository(_students),
                passwordGenerator,
                courseManager,
                emailManager);
        }

        [SetUp]
        public void Setup()
        {
            _studentBauknecht = new Student
            {
                Id = 13,
                Name = "Bauknecht",
                FirstName = "Ridder",
                TelephoneNumber = "12345687",
                Email = "ikbenbauknecht@mail.be",
                Role = RoleEnum.STUDENT,
                PasswordHash = null,
                PasswordSalt = null
            };
            _courseApplicatie = new Course
            {
                Id = 1,
                Name = "TI - Applicatie-ontwikkeling"
            };
            _internship = new Internship
            {
                Id = 1,
                Company = new Company
                {
                    Id = 100,
                    Name = "Krusty Krab",
                    AmountOfEmployees = 50,
                    AmountOfEmployeesIt = 5,
                    Address =
                        new Address
                        {
                            Street = "Mainstreet",
                            Number = "1",
                            ZipCode = "2090",
                            City = "BikiniBottom",
                            Country = "Ocean"
                        }
                },
                Title = "Application Development",
                Description =
                    "Say cheese cheesy grin blue castello. Brie feta cut the cheese cheesy grin cheesecake cheesy grin jarlsberg cheese and wine. Hard cheese cheeseburger bocconcini fondue pepper jack st. agur blue cheese jarlsberg cheese triangles. Macaroni cheese melted cheese emmental ricotta cheese and biscuits.",
                ContactPerson = new Person
                {
                    Id = 4,
                    Email = "contact@krustykrab.be",
                    Name = "Krabs",
                    FirstName = "Eugene",
                    Role = RoleEnum.CONTACT,
                    PasswordHash = null,
                    PasswordSalt = null
                },
                ResearchTopic =
                    "Port-salut cheese strings cauliflower cheese stilton pepper jack cut the cheese fromage frais caerphilly. ",
                Application = true,
                Résumée = true,
                StudentAmount = 1,
                State = InternshipStateEnum.APPROVED,
                NrOfSupportEmployees = 2
            };
            _students.Add(_studentBauknecht);
            _courses.Add(_courseApplicatie);
        }

        [TearDown]
        public void TearDown()
        {
            _courses.Clear();
            _students.Clear();
            _studentBauknecht = null;
            _courseApplicatie = null;
        }

        [Test]
        public async Task Succes()
        {
            await Sut.CreateLoginAsync(_studentBauknecht, CancellationToken.None);
            Assert.That(_studentBauknecht.PasswordHash, Is.Not.Null);
            Assert.That(_studentBauknecht.PasswordSalt, Is.Not.Null);
        }

        [Test]
        public async Task AddSucces()
        {
            _students.Clear();
            await Sut.CreateStudentAsync(_studentBauknecht, CancellationToken.None);
            Assert.That(_students.Contains(_studentBauknecht));
        }

        [Test]
        public async Task DeleteSucces()
        {
            Assert.That(_students.Contains(_studentBauknecht));
            await Sut.RemoveAsync(_studentBauknecht, CancellationToken.None);
            Assert.That(!_students.Contains(_studentBauknecht));
        }

        [Test]
        public async Task ToggleFavoriteSucces()
        {
            await Sut.ToggleFavorite(_studentBauknecht.Id, _internship, CancellationToken.None);
            StudentInternShip studentInternShip =
                _studentBauknecht.Favorites.SingleOrDefault(si => si.Internship == _internship);
            Assert.That(_studentBauknecht.Favorites, Is.Not.Null);
            Assert.That(_studentBauknecht.Favorites.Contains(studentInternShip));
            
        }

        [Test]
        public async Task RemoveToggleFavorite()
        {
            await Sut.ToggleFavorite(_studentBauknecht.Id, _internship, CancellationToken.None);
            await Sut.ToggleFavorite(_studentBauknecht.Id, _internship, CancellationToken.None);
            Assert.That(_studentBauknecht.Favorites, Is.Empty);
        }
    }
}
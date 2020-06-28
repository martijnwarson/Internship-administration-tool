using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using WebApplication.Enums;
using WebApplication.Helpers;
using WebApplication.Managers;
using WebApplication.Models;
using WebApplication.UnitTests.UnitTests.InMemoryRepositories;

namespace WebApplication.UnitTests.UnitTests.Managers
{
    public class InternShipManagerTests : SutTests<InternshipManager>
    {
        private readonly IList<Internship> _internships = new List<Internship>();
        private Internship _internship;
        private Person _contactEugene;
        private Company _companyKrusty;

        public InternShipManagerTests()
        {
            //IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            IPdfGenerator pdfGenerator = new PdfGenerator();
            Sut = new InternshipManager(new InMemoryInternshipRepository(_internships),pdfGenerator);
        }

        [SetUp]
        public void Setup()
        {
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
            _contactEugene = new Person
            {
                Id = 4,
                Email = "contact@krustykrab.be",
                Name = "Krabs",
                FirstName = "Eugene",
                Role = RoleEnum.CONTACT,
                PasswordHash = null,
                PasswordSalt = null
            };
            _companyKrusty = new Company
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
            };
            _internships.Add(_internship);
        }

        [TearDown]
        public void TearDown()
        {
            _internships.Clear();
            _internship = null;
        }

        [Test]
        public async Task AddSucces()
        {
            _internships.Clear();
            await Sut.AddAsync(_internship, CancellationToken.None);
            Assert.That(_internships , Is.Not.Null);
            Assert.That(_internships.Contains(_internship));
        }

        [Test]
        public async Task GetAllByContactIdSucces()
        {
            ISet<Internship> internships = 
                await Sut.GetAllInternshipsByContactId(_contactEugene.Id, CancellationToken.None);
            Assert.That(internships.Contains(internships.SingleOrDefault(i => i.ContactPerson.Id == _contactEugene.Id)));

          
        }

        [Test]
        public async Task GetAllInternshipsForStudentsSucces()
        {
            ISet<Internship> internships =
                await Sut.GetAllInternshipsForStudents(CancellationToken.None);
            Assert.That(internships.Contains(internships.SingleOrDefault(i => i.State == InternshipStateEnum.APPROVED)));
        }

        [Test]
        public async Task GetAllByStateSucces()
        {
            ISet<Internship> internships =
                await Sut.GetAllInternshipsByState(_internship.State, CancellationToken.None);

            Assert.That(internships.Contains(internships.SingleOrDefault(i => i.State == _internship.State)));
        }
        
        [Test]
        public async Task GetAllByCompanyIdSucces()
        {
            ISet<Internship> internships =
                await Sut.GetAllInternshipsByCompanyId(_companyKrusty.Id, CancellationToken.None);
            Assert.That(internships.Contains(internships.SingleOrDefault(i => i.Company.Id == _companyKrusty.Id)));
        }
    }
}
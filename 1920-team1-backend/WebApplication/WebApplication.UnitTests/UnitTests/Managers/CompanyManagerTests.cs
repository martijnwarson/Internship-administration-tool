using System.Collections.Generic;
using System.IO;
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
using WebApplication.UnitTests.UnitTests.Mocks;

namespace WebApplication.UnitTests.UnitTests.Managers
{
    public class CompanyManagerTests : SutTests<CompanyManager>
    {
        private readonly IList<Company> _companies = new List<Company>();
        private readonly IList<Person> _persons = new List<Person>();
        private Person _contactEugene;
        private Company _companyKrustykrab;

        public CompanyManagerTests()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            IPasswordGenerator passwordGenerator = new PasswordGenerator(configuration);
            IPdfGenerator pdfGenerator = new PdfGenerator();
            ISendGridService sendGridService = new SendGridService();
            IEmailManager emailManager = new EmailManager(sendGridService, pdfGenerator);
            Sut = 
                new CompanyManager(
                    new InMemoryCompanyRepository(_companies),
                    new PersonManager<Person>(
                        new InMemoryPersonRepository<Person>(_persons), passwordGenerator, emailManager),
                    new EmailManager(sendGridService, pdfGenerator),
                    pdfGenerator);
        }

        [SetUp]
        public void Setup()
        {
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
            _persons.Add(_contactEugene);
            
            _companyKrustykrab =
                new Company
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
                        },
                    ContactPerson = _contactEugene,
                    State = CompanyStateEnum.ACTIVE
                };
            _companies.Add(_companyKrustykrab);
        }

        [TearDown]
        public void TearDown()
        {
            _companies.Clear();
            _persons.Clear();
            _contactEugene = null;
            _companyKrustykrab = null;
        }

        [Test]
        public async Task ActivateSuccess()
        {
            // Arrange
            _companyKrustykrab.State = CompanyStateEnum.NEW;
            
            // Act
            await Sut.ActivateCompanyAsync(_companyKrustykrab, CancellationToken.None);

            // Assert
            Assert.That(_companyKrustykrab.State, Is.EqualTo(CompanyStateEnum.ACTIVE));
            Assert.That(_contactEugene.PasswordHash, Is.Not.Null);
            Assert.That(_contactEugene.PasswordSalt, Is.Not.Null);
        }
        
        [Test]
        public async Task DeactivateSuccess()
        {
            // Arrange

            // Act
            await Sut.DeactivateCompanyAsync(_companyKrustykrab, CancellationToken.None);

            // Assert
            Assert.That(_companyKrustykrab.State, Is.EqualTo(CompanyStateEnum.INACTIVE));
        }
        
        [Test]
        public async Task GetAllByStateNewSuccess()
        {
            // Arrange
            _companyKrustykrab.State = CompanyStateEnum.NEW;

            // Act
            ISet<Company> companies = await Sut.GetAllByStateAsync(CompanyStateEnum.NEW, CancellationToken.None);

            // Assert
            Assert.That(companies.Contains(_companyKrustykrab));
        }
        
        [Test]
        public async Task GetAllByStateActiveSuccess()
        {
            // Arrange
            _companyKrustykrab.State = CompanyStateEnum.ACTIVE;

            // Act
            ISet<Company> companies = await Sut.GetAllByStateAsync(CompanyStateEnum.ACTIVE, CancellationToken.None);

            // Assert
            Assert.That(companies.Contains(_companyKrustykrab));
        }
        
        [Test]
        public async Task GetAllByStateInactieSuccess()
        {
            // Arrange
            _companyKrustykrab.State = CompanyStateEnum.INACTIVE;

            // Act
            ISet<Company> companies = await Sut.GetAllByStateAsync(CompanyStateEnum.INACTIVE, CancellationToken.None);

            // Assert
            Assert.That(companies.Contains(_companyKrustykrab));
        }
    }
}
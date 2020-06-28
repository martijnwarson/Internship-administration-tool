using System;
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

namespace WebApplication.UnitTests.UnitTests.Helpers
{
    [TestFixture]
    public class UserVerifierTests
        : SutTests<UserVerifier>
    {
        private readonly IList<Company> _companies = new List<Company>();
        private Internship _internship;
        private Validation _validation;
        private readonly IList<Person> _persons = new List<Person>();
        private Company _companyKrustykrab;
        private Person _contactEugene;
        private Person _contactPlankton;
        private Coördinator _coördinatorGary;
        private Lector _lectorPatchy;
        private Lector _lectorPotty;

        private Student student_spongebob;

        public UserVerifierTests()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            IPasswordGenerator passwordGenerator = new PasswordGenerator(configuration);
            IPdfGenerator pdfGenerator = new MockPdfGenerator();
            ISendGridService sendGridService = new SendGridService();
            IEmailManager emailManager = new EmailManager(sendGridService, pdfGenerator);
            PersonManager<Person> personManager =
                new PersonManager<Person>(new InMemoryPersonRepository<Person>(_persons), passwordGenerator, emailManager);
            Sut = new UserVerifier(personManager,
                new CompanyManager(new InMemoryCompanyRepository(_companies), personManager, emailManager, pdfGenerator));
        }

        [SetUp]
        public void Setup()
        {
            student_spongebob = new Student
            {
                Id = 1,
                Name = "SquarePants",
                FirstName = "SpongeBob",
                Address =
                    new Address
                    {
                        Street = "Mainstreet",
                        Number = "5",
                        ZipCode = "2090",
                        City = "BikiniBottom",
                        Country = "Ocean"
                    },
                Course = new Course {Name = "TI - Applicatie-ontwikkeling"},
                Email = "student1@pxl.be",
                Role = RoleEnum.STUDENT,
                TelephoneNumber = "012/34.56.78"
            };
            _persons.Add(student_spongebob);

            _coördinatorGary = new Coördinator
            {
                Id = 2,
                Email = "admin@pxl.be",
                Name = "the Snail",
                FirstName = "Gary",
                Role = RoleEnum.COÖRDINATOR
            };
            _persons.Add(_coördinatorGary);

            _lectorPatchy = new Lector
            {
                Id = 3,
                Email = "lector1@pxl.be",
                Name = "the Pirate",
                FirstName = "Patchy",
                Role = RoleEnum.LECTOR
            };
            _persons.Add(_lectorPatchy);
            
            _lectorPotty = new Lector
            {
                Email = "lector2@pxl.be",
                Name = "the Parrot",
                FirstName = "Potty",
                Role = RoleEnum.LECTOR
            };
            _persons.Add(_lectorPotty);

            _contactEugene = new Person
            {
                Id = 4,
                Email = "contact@krustykrab.be",
                Name = "Krabs",
                FirstName = "Eugene",
                Role = RoleEnum.CONTACT
            };
            _persons.Add(_contactEugene);
            
            _contactPlankton = new Person
            {
                Id = 5,
                Email = "contact@chumbucket.be",
                Name = "Plankton",
                FirstName = "Plankton",
                Role = RoleEnum.CONTACT
            };
            _persons.Add(_contactPlankton);

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

            _internship =
                new Internship
                {
                    Company = _companyKrustykrab,
                    Title = "Application Development @ Krusty Krab",
                    Description =
                        "Say cheese cheesy grin blue castello. Brie feta cut the cheese cheesy grin cheesecake cheesy grin jarlsberg cheese and wine. Hard cheese cheeseburger bocconcini fondue pepper jack st. agur blue cheese jarlsberg cheese triangles. Macaroni cheese melted cheese emmental ricotta cheese and biscuits.",
                    ContactPerson = _contactEugene,
                    Address = _companyKrustykrab.Address,
                    TechDescription = "Danish fontina rubber cheese bavarian bergkase",
                    ResearchTopic =
                        "Port-salut cheese strings cauliflower cheese stilton pepper jack cut the cheese fromage frais caerphilly. ",
                    Application = true,
                    Résumée = true,
                    Reimbursement = true,
                    StudentAmount = 1,
                    Remarks = "",
                    State = InternshipStateEnum.NEW
                };
            
            _validation = new Validation
            {
                Internship = _internship,
                Lector = _lectorPatchy,
                State = ValidationStateEnum.APPROVED,
                Date = DateTime.Now,
                FeedBack = new FeedBack{ Value = " jsdhffgrfgmkrkgkgkerg"}
            };
            
            _internship.Validations.Add(_validation);
        }

        [TearDown]
        public void OnTeardown()
        {
            _companies.Clear();
            _persons.Clear();
            student_spongebob = null;
            _coördinatorGary = null;
            _companyKrustykrab = null;
            _internship = null;
            _contactEugene = null;
            _contactPlankton = null;
            _lectorPatchy = null;
        }

        [Test]
        public void AssertStudentNewInternship()
        {
            // Arrange
            _internship.State = InternshipStateEnum.NEW;

            // Act
            var task = Sut.VerifyUserAsync(student_spongebob.Id, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertStudentPendingInternship()
        {
            // Arrange
            _internship.State = InternshipStateEnum.PENDING;

            // Act
            var task = Sut.VerifyUserAsync(student_spongebob.Id, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertStudentModifiedInternship()
        {
            // Arrange
            _internship.State = InternshipStateEnum.MODIFIED;

            // Act
            var task = Sut.VerifyUserAsync(student_spongebob.Id, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertStudentRejectedInternship()
        {
            // Arrange
            _internship.State = InternshipStateEnum.REJECTED;

            // Act
            var task = Sut.VerifyUserAsync(student_spongebob.Id, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertStudentToBeModifiedInternship()
        {
            // Arrange
            _internship.State = InternshipStateEnum.TO_BE_MODIFIED;

            // Act
            var task = Sut.VerifyUserAsync(student_spongebob.Id, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertStudentApprovedInternship()
        {
            // Arrange
            _internship.State = InternshipStateEnum.APPROVED;

            // Act
            var task = Sut.VerifyUserAsync(student_spongebob.Id, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void AssertCoordinatorInternship()
        {
            // Arrange
            _internship.State = InternshipStateEnum.APPROVED;

            // Act
            var task = Sut.VerifyUserAsync(_coördinatorGary.Id, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void AssertContactPersonOfCompanyInternship()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_contactEugene.Id, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public async Task AssertContactPersonNotOfCompanyInternship()
        {
            // Arrange

            // Act
            var result = await Sut.VerifyUserAsync(_contactPlankton.Id, _internship, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertNonUserInternship()
        {
            // Arrange
            long NonExistingUserId = 123456789;
            
            // Act
            var task = Sut.VerifyUserAsync(NonExistingUserId, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertLectorWithValidationInternship()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_lectorPatchy.Id, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void AssertLectorWithoutValidationInternship()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_lectorPotty.Id, _internship, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertLectorOnValidation()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_lectorPatchy.Id, _validation, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void AssertLectorNotOnValidation()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_lectorPotty.Id, _validation, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertCoordinatorValidation()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_coördinatorGary.Id, _validation, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void AssertStudentValidation()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(student_spongebob.Id, _validation, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertContactValidation()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_contactEugene.Id, _validation, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertContactOfCompany()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_contactEugene.Id, _companyKrustykrab, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void AssertContactNotOfCompany()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_contactPlankton.Id, _companyKrustykrab, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertCoordinatorCompany()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_coördinatorGary.Id, _companyKrustykrab, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void AssertStudentCompany()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(student_spongebob.Id, _companyKrustykrab, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void AssertLectorCompany()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserAsync(_lectorPatchy.Id, _companyKrustykrab, CancellationToken.None);
            var result = task.Result;

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void GetCompanyByUser()
        {
            // Arrange

            // Act
            var task = Sut.VerifyUserCompanyAsync(_contactEugene, CancellationToken.None);
            Company result = task.Result;

            // Assert
            Assert.That(result, Is.EqualTo(_companyKrustykrab));
        }
    }
}
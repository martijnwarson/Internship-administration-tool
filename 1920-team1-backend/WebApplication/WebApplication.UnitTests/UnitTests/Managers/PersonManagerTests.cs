using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
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
    public class PersonManagerTests : SutTests<PersonManager<Person>>
    {
        private readonly IList<Person> _persons = new List<Person>();
        private Person _contactEugene;
        
        public PersonManagerTests()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            IPasswordGenerator passwordGenerator = new PasswordGenerator(configuration);
            IPdfGenerator pdfGenerator = new PdfGenerator();
            ISendGridService sendGridService = new SendGridService();
            IEmailManager emailManager = new EmailManager(sendGridService, pdfGenerator);
            Sut = new PersonManager<Person>(new InMemoryPersonRepository<Person>(_persons), passwordGenerator, emailManager);
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
        }

        [TearDown]
        public void TearDown()
        {
            _persons.Clear();
            _contactEugene = null;
        }

        [Test]
        public async Task Success()
        { 
            await Sut.CreateLoginAsync(_contactEugene, CancellationToken.None);
            Assert.That(_contactEugene.PasswordHash, Is.Not.Null);
            Assert.That(_contactEugene.PasswordSalt, Is.Not.Null);
        }
        
        [Test]
        public async Task AddSuccess()
        { 
            _persons.Clear();
            await Sut.AddAsync(_contactEugene, CancellationToken.None);
            Assert.That(_persons.Contains(_contactEugene));
        }
        
        [Test]
        public async Task DeleteSuccess()
        {
            Assert.That(_persons.Contains(_contactEugene));
            await Sut.RemoveAsync(_contactEugene, CancellationToken.None);
            Assert.That(!_persons.Contains(_contactEugene));
        }
    }
}
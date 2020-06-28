
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

namespace WebApplication.UnitTests.UnitTests.Managers
{

    public class LectorManagerTests : SutTests<LectorManager<Lector>>
    {
        private readonly IList<Lector> _lectors = new List<Lector>();
        private Lector _lectorPatchy;

        public LectorManagerTests()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            IPasswordGenerator passwordGenerator = new PasswordGenerator(configuration);
            ISendGridService sendGridService = new SendGridService();
            IPdfGenerator pdfGenerator = new PdfGenerator();
            IEmailManager emailManager = new EmailManager(sendGridService, pdfGenerator);
            Sut = new LectorManager<Lector>(new InMemoryLectorRepository<Lector>(_lectors), passwordGenerator, emailManager);
        }

        [SetUp]
        public void Setup()
        {
            _lectorPatchy = new Lector
            {
                Id = 6,
                Name = "The Pirate",
                FirstName = "Pathcy",
                Email = "lector1@pxl.be",
                Role = RoleEnum.LECTOR,
                PasswordHash = null,
                PasswordSalt = null,
                Courses = null,
            };
            _lectors.Add(_lectorPatchy);
        }

        [TearDown]
        public void TearDown()
        {
            _lectors.Clear();
            _lectorPatchy = null;
        }

        [Test]
        public async Task Succes()
        {
            await Sut.CreateLoginAsync(_lectorPatchy, CancellationToken.None);
            Assert.That(_lectorPatchy.PasswordHash, Is.Not.Null);
            Assert.That(_lectorPatchy.PasswordSalt, Is.Not.Null);
        }

        [Test]
        public async Task AddSucces()
        {
            _lectors.Clear();
            await Sut.CreateLectorAsync(_lectorPatchy, CancellationToken.None);
            Assert.That(_lectors.Contains(_lectorPatchy));
        }

        [Test]
        public async Task DeleteSucces()
        {
            Assert.That(_lectors.Contains(_lectorPatchy));
            await Sut.RemoveAsync(_lectorPatchy, CancellationToken.None);
            Assert.That(!_lectors.Contains(_lectorPatchy));
        }
    }
}

using System.IO;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.UnitTests.UnitTests.Mocks
{
    public class MockPdfGenerator : IPdfGenerator
    {
        public FileStreamResult CreateCompanyPdf(Company company)
        {
            // NOP
            return new FileStreamResult(new FileStream(string.Empty, FileMode.OpenOrCreate), string.Empty);
        }

        public FileStreamResult CreateInternshipPdf(Internship internship)
        {
            return new FileStreamResult(new FileStream(string.Empty, FileMode.OpenOrCreate), string.Empty);
        }
    }
}
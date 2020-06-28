using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Helpers
{
    public interface IPdfGenerator
    {
        FileStreamResult CreateCompanyPdf(Company company);
        FileStreamResult CreateInternshipPdf(Internship internship);
    }
}
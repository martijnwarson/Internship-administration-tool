using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.EmailService
{
    public interface ISendGridService
    {
        Task SendMailAsync(string body, string mailTo, string mailToName, string mailSubject);
        Task SendMailAsync(string body, string mailTo, string mailToName, string mailSubject, FileStreamResult attachment);
    }
}

using System.Threading;
using System.Threading.Tasks;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Helpers
{
    public interface IUserVerifier
    {
        Task<Company> VerifyUserCompanyAsync(Person user, CancellationToken cancellationToken);

        Task<bool> VerifyUserAsync(long userId, Internship internship, CancellationToken cancellationToken);

        Task<bool> VerifyUserAsync(long userId, Validation validation, CancellationToken cancellationToken);
        
        Task<bool> VerifyUserAsync(long userId, Company company, CancellationToken cancellationToken);
        Task<bool> VerifyUserAsync(long userId, InternshipStateEnum stateEnum, CancellationToken cancellationToken);
    }
}
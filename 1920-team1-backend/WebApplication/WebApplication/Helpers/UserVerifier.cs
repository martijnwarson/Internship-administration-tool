#nullable enable
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Enums;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Helpers
{
    public class UserVerifier
        : IUserVerifier
    {
        public UserVerifier(
            IPersonManager<Person> personManager,
            ICompanyManager companyManager)
        {
            PersonManager = personManager;
            CompanyManager = companyManager;
        }

        private IPersonManager<Person> PersonManager { get; }

        private ICompanyManager CompanyManager { get; }

        private async Task<bool> VerifyUserCompanyAsync(Person user, Company? company,
            CancellationToken cancellationToken)
        {
            Company foundCompany = await VerifyUserCompanyAsync(user, cancellationToken);
            return company == foundCompany && foundCompany?.State == CompanyStateEnum.ACTIVE;
        }

        public async Task<Company> VerifyUserCompanyAsync(Person user, CancellationToken cancellationToken)
            => await CompanyManager.GetCompanyByContactAsync(user, cancellationToken);

        public async Task<bool> VerifyUserAsync(long userId, Internship internship, CancellationToken cancellationToken)
        {
            Person user = await PersonManager.GetByIdAsync(userId, cancellationToken);
            return user?.Role switch
            {
                RoleEnum.CONTACT when await VerifyUserCompanyAsync(user, internship.Company, cancellationToken) =>
                true,
                RoleEnum.LECTOR when internship.Validations.Any(v => v.Lector == user) => true,
                RoleEnum.COÖRDINATOR => true,
                RoleEnum.STUDENT when internship.State == InternshipStateEnum.APPROVED => true,
                _ => false
            };
        }

        public async Task<bool> VerifyUserAsync(long userId, Validation validation, CancellationToken cancellationToken)
        {
            Person user = await PersonManager.GetByIdAsync(userId, cancellationToken);
            return user?.Role switch
            {
                RoleEnum.LECTOR when validation.Lector == user => true,
                RoleEnum.COÖRDINATOR => true,
                _ => false
            };
        }

        public async Task<bool> VerifyUserAsync(long userId, Company? company, CancellationToken cancellationToken)
        {
            Person user = await PersonManager.GetByIdAsync(userId, cancellationToken);
            return user?.Role switch
            {
                RoleEnum.CONTACT when await VerifyUserCompanyAsync(user, company, cancellationToken) =>
                true,
                RoleEnum.COÖRDINATOR => true,
                _ => false
            };
        }
        
        public async Task<bool> VerifyUserAsync(long userId, InternshipStateEnum stateEnum, CancellationToken cancellationToken)
        {
            Person user = await PersonManager.GetByIdAsync(userId, cancellationToken);
            return user?.Role switch
            {
                RoleEnum.COÖRDINATOR => true,
                RoleEnum.STUDENT when stateEnum == InternshipStateEnum.APPROVED=> true,
                _ => false
            };
        }
    }
}
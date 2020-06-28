using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Enums;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.UnitTests.UnitTests.InMemoryRepositories
{
    /// <inheritdoc cref="ICompanyRepository"/>
    public class InMemoryCompanyRepository
        : InMemoryRepository<Company>,
            ICompanyRepository
    {
        public async Task<Company> GetCompanyByContactAsync(Person contactPerson, CancellationToken cancellationToken)
            => await Factory.StartNew(() => Models.SingleOrDefault(c => c.ContactPerson == contactPerson), cancellationToken);

        public async Task<IList<Company>> GetAllByStateAsync(CompanyStateEnum stateEnum,
            CancellationToken cancellationToken) =>
            await Factory.StartNew(() => Models.Where(c => c.State == stateEnum).ToList(), cancellationToken);

        public InMemoryCompanyRepository(IList<Company> models)
            : base(models)
        {
        }
    }
}
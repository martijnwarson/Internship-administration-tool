using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Enums;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    /// <inheritdoc cref="ICompanyRepository"/>
    public class CompanyRepository
        : CrudRepository<Company>,
            ICompanyRepository
    {
        public CompanyRepository(DataContext dataContext)
            : base(dataContext)
        {
        }

        public async Task<Company> GetCompanyByContactAsync(Person contactPerson, CancellationToken cancellationToken)
        {
            return await DataContext
                .Companies
                .Include(c => c.Address)
                .Include(c => c.ContactPerson)
                .SingleOrDefaultAsync(c => c.ContactPerson == contactPerson, cancellationToken: cancellationToken);
        }

        public override async Task<Company> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await DataContext
                .Companies
                .Include(c => c.Address)
                .Include(c => c.ContactPerson)
                .SingleOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
        }

        public override async Task<IList<Company>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await DataContext
                .Companies
                .Include(c => c.Address)
                .Include(c => c.ContactPerson)
                .ToListAsync(cancellationToken);
        }
        
        public async Task<IList<Company>> GetAllByStateAsync(CompanyStateEnum stateEnum, CancellationToken cancellationToken)
        {
            return await DataContext
                .Companies
                .Include(c => c.Address)
                .Include(c => c.ContactPerson)
                .Where(c => c.State == stateEnum)
                .ToListAsync(cancellationToken);
        }
    }
}
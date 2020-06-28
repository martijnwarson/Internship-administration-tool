using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class ValidationRepository
        : CrudRepository<Validation>,
            IValidationRepository
    {
        public ValidationRepository(DataContext datacontext)
            : base(datacontext)
        {
        }

        public override async Task<IList<Validation>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await DataContext
                .Set<Validation>()
                .Include(v => v.FeedBack)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
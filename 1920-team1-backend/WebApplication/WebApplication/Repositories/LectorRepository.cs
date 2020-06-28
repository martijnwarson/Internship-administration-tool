using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    /// <inheritdoc cref="ILectorRepository{TModel}"/>
    public class LectorRepository<TModel>
        : PersonRepository<TModel>,
            ILectorRepository<TModel>
    where TModel : Lector
    {
        public LectorRepository(DataContext dataContext)
            : base(dataContext)
        {
        }

        public override async Task<TModel> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return (TModel) await DataContext
                .Set<Lector>()
                .Include(l => l.Courses)
                .ThenInclude(lc => lc.Course)
                .SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
        }

        public override async Task<IList<TModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return (IList<TModel>) await DataContext
                .Set<Lector>()
                .Include(l => l.Courses)
                .ThenInclude(lc => lc.Course)
                .ToListAsync();
        }
    }
}
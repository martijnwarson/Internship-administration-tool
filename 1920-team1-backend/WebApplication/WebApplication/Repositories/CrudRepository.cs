using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    /// <inheritdoc cref="ICrudRepository{TModel}"/>
    public class CrudRepository<TModel> : ICrudRepository<TModel>
        where TModel : BaseModel
    {
        public CrudRepository(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        protected DataContext DataContext { get; }

        public virtual async Task<IList<TModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await DataContext.Set<TModel>().ToListAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<TModel> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await DataContext.FindAsync<TModel>(id);
        }

        public virtual async Task AddAsync(TModel entity, CancellationToken cancellationToken)
        {
            await DataContext.AddAsync(entity, cancellationToken);
            await DataContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(TModel entity, CancellationToken cancellationToken)
        {
            DataContext.Update(entity);
            await DataContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task RemoveAsync(long id, CancellationToken cancellationToken)
        {
            var toDelete = await DataContext.FindAsync<TModel>(id);
            DataContext.Remove(toDelete);
            await DataContext.SaveChangesAsync(cancellationToken);
        }
        
        public virtual async Task RemoveAsync(TModel entity, CancellationToken cancellationToken)
        {
            DataContext.Remove(entity);
            await DataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
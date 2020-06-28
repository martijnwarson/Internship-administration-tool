using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="IModelManager{TModel}"/>
    public abstract class ModelManager<TModel>
        : IModelManager<TModel>
        where TModel : BaseModel
    {
        protected ICrudRepository<TModel> Repository { get; }

        protected ModelManager(ICrudRepository<TModel> repository)
        {
            Repository = repository;
        }
        
        
        public virtual async Task<ISet<TModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            IList<TModel> models = await Repository.GetAllAsync(cancellationToken);
            return models.ToHashSet();
        }

        public virtual async Task<TModel> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await Repository.GetByIdAsync(id, cancellationToken);
        }

        public virtual Task AddAsync(TModel entity, CancellationToken cancellationToken)
        {
            return Repository.AddAsync(entity, cancellationToken);
        }

        public virtual Task UpdateAsync(TModel entity, CancellationToken cancellationToken)
        {
            return Repository.UpdateAsync(entity, cancellationToken);
        }

        public virtual Task RemoveAsync(long id, CancellationToken cancellationToken)
        {
            return Repository.RemoveAsync(id, cancellationToken);
        }

        public virtual Task RemoveAsync(TModel entity, CancellationToken cancellationToken)
        {
            return Repository.RemoveAsync(entity, cancellationToken);
        }
    }
}
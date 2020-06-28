using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Managers.Interfaces
{
    public interface IModelManager<TModel>
        where TModel : BaseModel
    {
        /// <summary>
        ///     async method to retrieve all <see cref="TModel"/>s.
        /// </summary>
        /// <param name="cancellationToken">the token to cancel the request</param>
        /// <returns>a <see cref="Task{TResult}" /> that returns an <see cref="ISet{T}"/> <see cref="TModel" /></returns>
        Task<ISet<TModel>> GetAllAsync(CancellationToken cancellationToken);
        
        /// <summary>
        ///     async method to retrieve a <see cref="TModel" /> with the given <paramref name="id" />.
        /// </summary>
        /// <param name="id">the primary key of the <see cref="TModel" /></param>
        /// <param name="cancellationToken">the token to cancel the request</param>
        /// <returns>a <see cref="Task{TResult}" /> that returns a <see cref="TModel" /></returns>
        Task<TModel> GetByIdAsync(long id, CancellationToken cancellationToken);
        
        /// <summary>
        ///     Add a <see cref="TModel" /> to the database.
        /// </summary>
        /// <param name="entity">The given <see cref="TModel" /> to save to the database.</param>
        /// <param name="cancellationToken">the token to cancel the request</param>
        Task AddAsync(TModel entity, CancellationToken cancellationToken);
        
        /// <summary>
        ///     Update a <see cref="TModel" /> in the database.
        /// </summary>
        /// <param name="entity">The given <see cref="TModel" /> to update to the database.</param>
        /// <param name="cancellationToken">the token to cancel the request</param>
        Task UpdateAsync(TModel entity, CancellationToken cancellationToken);
        
        /// <summary>
        ///     Remove a <see cref="TModel" /> from the database based on the primary Key
        /// </summary>
        /// <param name="id">the given primary key</param>
        /// <param name="cancellationToken">the token to cancel the request</param>
        Task RemoveAsync(long id, CancellationToken cancellationToken);
        
        /// <summary>
        ///     Remove a <see cref="TModel" /> from the database based on the primary Key
        /// </summary>
        /// <param name="entity">The given <see cref="TModel" /> to remove from the database.</param>
        /// <param name="cancellationToken">the token to cancel the request</param>
        Task RemoveAsync(TModel entity, CancellationToken cancellationToken);
    }
}
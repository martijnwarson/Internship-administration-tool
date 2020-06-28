using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface ICrudRepository<TModel>
        where TModel : BaseModel
    {
        /// <summary>
        ///     Returns all <see cref="TModel" />s from the database
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request</param>
        /// <returns>
        ///     a <see cref="Task{TResult}" /> that returns an <see cref="IList{T}" /> of <see cref="TModel" />
        /// </returns>
        Task<IList<TModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///     Returns a <see cref="TModel" /> with the given <paramref name="id" />.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request</param>
        /// <param name="id">the primary key of the <see cref="TModel" /></param>
        /// <returns>a <see cref="Task{TResult}" /> that returns a <see cref="TModel" /></returns>
        Task<TModel> GetByIdAsync(long id, CancellationToken cancellationToken);

        /// <summary>
        ///     Add a <see cref="TModel" /> to the database.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request</param>
        /// <param name="entity">The given <see cref="TModel" /> to save to the database.</param>
        Task AddAsync(TModel entity, CancellationToken cancellationToken);

        /// <summary>
        ///     Update a <see cref="TModel" /> in the database.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request</param>
        /// <param name="entity">The given <see cref="TModel" /> to update to the database.</param>
        Task UpdateAsync(TModel entity, CancellationToken cancellationToken);

        /// <summary>
        ///     Remove a <see cref="TModel" /> from the database based on the primary Key
        /// </summary>
        /// <param name="id">the given primary key</param>
        /// <param name="cancellationToken">Token to cancel the request</param>
        Task RemoveAsync(long id, CancellationToken cancellationToken);

        /// <summary>
        ///     Remove a <see cref="TModel" /> from the database based on the primary Key
        /// </summary>
        /// <param name="entity">The given <see cref="TModel" /> to remove from the database.</param>
        /// <param name="cancellationToken">Token to cancel the request</param>
        Task RemoveAsync(TModel entity, CancellationToken cancellationToken);
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Managers.Interfaces
{
    /// <inheritdoc cref="IModelManager{TModel}" />
    public interface ICourseManager
        : IModelManager<Course>
    {
        /// <summary>
        ///     Get all Courses for the given Ids
        /// </summary>
        /// <param name="ids">The given Ids</param>
        /// <param name="cancellationToken">Token to cancel the request</param>
        /// <returns>A <see cref="Task{TResult}"/> to get An <see cref="ISet{T}"/> of <see cref="Course"/>s</returns>
        Task<ISet<Course>> GetByIdsAsync(IEnumerable<long> ids, CancellationToken cancellationToken);
    }
}
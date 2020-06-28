using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers.Interfaces
{
    /// <inheritdoc cref="IPersonManager{TModel}"/>
    public interface ILectorManager<TModel>
        : IPersonManager<TModel>
        where TModel : Lector
    {
        public Task<ISet<Lector>> GetLectorsByIdsAsync(IEnumerable<long> lectorIds, CancellationToken cancellationToken);

        public Task<(string username, string password)> CreateLectorAsync(TModel lector, CancellationToken cancellationToken);
    }
}
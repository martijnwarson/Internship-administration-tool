using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Managers.Interfaces
{
    /// <inheritdoc cref="IModelManager{TModel}" />
    public interface IValidationManager
        : IModelManager<Validation>
    {
        /// <summary>
        ///     Create <see cref="Validation"/>s for the given <see cref="Internship"/> all given <see cref="Lector"/>s.
        /// </summary>
        /// <param name="internship">the given <see cref="Internship"/></param>
        /// <param name="lectors">the given <see cref="Lector"/>s</param>
        /// <param name="cancellationToken">Token to cancel the request</param>
        public Task CreateValidationsByInternshipAndLectorsAsync(Internship internship, IEnumerable<Lector> lectors, CancellationToken cancellationToken);
    }
}
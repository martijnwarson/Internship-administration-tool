using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Managers.Interfaces
{
    /// <inheritdoc cref="IModelManager{TModel}" />
    public interface IPersonManager<TModel> : IModelManager<TModel>
        where TModel : Person
    {
        /// <summary>
        ///      Create password and salt for the given <inheritdoc cref="Person"/>.
        /// </summary>
        /// <param name="person">The given <see cref="Person"/>.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>the password</returns>
        Task<string> CreateLoginAsync(TModel person, CancellationToken cancellationToken);
    }
}
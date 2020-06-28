using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    /// <inheritdoc cref="ICrudRepository{TModel}"/>
    public interface IValidationRepository
        : ICrudRepository<Validation>
    {
    }
}
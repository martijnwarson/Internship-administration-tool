using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    /// <inheritdoc cref="ICrudRepository{TModel}"/>
    public interface IPersonRepository<TModel> 
        : ICrudRepository<TModel>
        where TModel : Person
    {
    }
}
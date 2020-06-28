using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    /// <inheritdoc cref="IPersonRepository{TModel}"/>
    public class PersonRepository<TModel>
        : CrudRepository<TModel>,
            IPersonRepository<TModel>
    where TModel : Person
    {
        public PersonRepository(DataContext dataContext)
            : base(dataContext)
        {
        }
    }
}
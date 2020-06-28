using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    /// <inheritdoc cref="ITechnologyRepository" />
    public class TechnologyRepository
        : CrudRepository<Technology>,
            ITechnologyRepository
    {
        public TechnologyRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
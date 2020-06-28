using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    /// <inheritdoc cref="ICoördinatorRepository" />
    public class CoördinatorRepository
        : LectorRepository<Coördinator>,
            ICoördinatorRepository
    {
        public CoördinatorRepository(DataContext dataContext)
            : base(dataContext)
        {
        }
    }
}
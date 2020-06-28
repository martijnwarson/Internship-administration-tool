using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    /// <inheritdoc cref="IPeriodRepository" />
    public class PeriodRepository
        : CrudRepository<Period>,
            IPeriodRepository
    {
        public PeriodRepository(DataContext dataContext)
            : base(dataContext)
        {
        }
    }
}
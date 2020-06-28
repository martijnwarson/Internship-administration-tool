using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="IPeriodManager"/>
    public class PeriodManager
        : ModelManager<Period>,
            IPeriodManager
    {
        public PeriodManager(ICrudRepository<Period> repository)
            : base(repository)
        {
        }
    }
}
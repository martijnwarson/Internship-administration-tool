using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="ITechnologyManager"/>>
    public class TechnologyManager
        : ModelManager<Technology>,
            ITechnologyManager
    {
        public TechnologyManager(ICrudRepository<Technology> repository)
            : base(repository)
        {
        }
    }
}
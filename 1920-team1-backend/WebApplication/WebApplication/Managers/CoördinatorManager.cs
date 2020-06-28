using WebApplication.Helpers;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="CoördinatorManager"/>
    public class CoördinatorManager
        : LectorManager<Coördinator>,
            ICoördinatorManager
    {
        public CoördinatorManager(
            ICoördinatorRepository repository,
            IPasswordGenerator passwordGenerator,
            IEmailManager emailManager)
            : base(repository, passwordGenerator, emailManager)
        {
        }
    }
}
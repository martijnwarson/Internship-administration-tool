using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Enums;
using WebApplication.Helpers;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="ILectorManager{TModel}"/>
    public class LectorManager<TModel>
        : PersonManager<TModel>,
            ILectorManager<TModel>
        where TModel : Lector
    {
        public LectorManager(
            ILectorRepository<TModel> repository,
            IPasswordGenerator passwordGenerator,
            IEmailManager emailManager) 
            : base(repository, passwordGenerator, emailManager)
        {
        }

        public async Task<ISet<Lector>> GetLectorsByIdsAsync(IEnumerable<long> lectorIds, CancellationToken cancellationToken)
        {
            ISet<Lector> lectors = new HashSet<Lector>();
            foreach (var lectorId in lectorIds)
            {
                lectors.Add(await GetByIdAsync(lectorId, cancellationToken));
            }

            return lectors;
        }

        public async Task<(string username, string password)> CreateLectorAsync(TModel lector, CancellationToken cancellationToken)
        {
            lector.Role = RoleEnum.LECTOR;
            string password =
                await CreateLoginAsync(
                    lector,
                    cancellationToken);
            return (lector.Email, password);
        }
    }
}
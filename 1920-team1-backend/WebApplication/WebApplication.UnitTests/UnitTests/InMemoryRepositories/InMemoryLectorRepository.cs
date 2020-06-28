using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.UnitTests.UnitTests.InMemoryRepositories
{
    public class InMemoryLectorRepository<TLector>
        :InMemoryRepository<TLector>,
            ILectorRepository<TLector>
        where TLector : Lector
    {
        public InMemoryLectorRepository(IList<TLector> models) : base(models)
        {
        }
    }
}
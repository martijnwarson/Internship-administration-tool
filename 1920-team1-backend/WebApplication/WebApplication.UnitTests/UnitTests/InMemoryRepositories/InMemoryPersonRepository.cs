using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.UnitTests.UnitTests.InMemoryRepositories
{
    /// <inheritdoc cref="IPersonRepository{TModel}"/>
    public class InMemoryPersonRepository<TPerson>
        : InMemoryRepository<TPerson>,
            IPersonRepository<TPerson>
        where TPerson : Person
    {
        public InMemoryPersonRepository(IList<TPerson> models) : base(models)
        {
        }
    }
}
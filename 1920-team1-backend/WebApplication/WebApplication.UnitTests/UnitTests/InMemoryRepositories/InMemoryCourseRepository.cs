using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.UnitTests.UnitTests.InMemoryRepositories
{
    public class InMemoryCourseRepository
        :InMemoryRepository<Course>,
            ICourseRepository
    {
        public InMemoryCourseRepository(IList<Course> models) : base(models)
        {
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Managers;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.UnitTests.UnitTests.InMemoryRepositories
{
    public class InMemoryStudentRepository
        : InMemoryRepository<Student>,
            IStudentRepository

    {
        public InMemoryStudentRepository(IList<Student> models) : base(models)
        {
        }
    }
}
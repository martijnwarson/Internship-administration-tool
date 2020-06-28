using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    /// <inheritdoc cref="IPersonRepository{TModel}" />
    public interface IStudentRepository 
        : IPersonRepository<Student>
    {
    }
}
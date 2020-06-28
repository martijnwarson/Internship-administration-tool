using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    /// <inheritdoc cref="IStudentRepository" />
    public class StudentRepository
        : PersonRepository<Student>,
            IStudentRepository
    {
        public StudentRepository(DataContext dataContext)
            : base(dataContext)
        {
        }

        public override async Task<Student> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await DataContext
                .Set<Student>()
                .Include(s => s.Course)
                .Include(s => s.Address)
                .Include(s => s.Favorites)
                .ThenInclude(f => f.Internship)
                .ThenInclude(i => i.Company)
                .SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public override async Task<IList<Student>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await DataContext
                .Set<Student>()
                .Include(s => s.Course)
                .ToListAsync();
        }
    }
}
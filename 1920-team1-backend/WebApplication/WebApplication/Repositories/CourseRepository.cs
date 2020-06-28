using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    /// <inheritdoc cref="ICourseRepository" />
    public class CourseRepository
        : CrudRepository<Course>,
            ICourseRepository
    {
        public CourseRepository(DataContext dataContext)
            : base(dataContext)
        {
        }
    }
}
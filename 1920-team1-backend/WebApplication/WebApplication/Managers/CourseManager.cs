using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="ICourseManager"/>
    public class CourseManager
        : ModelManager<Course>,
            ICourseManager
    {
        public CourseManager(ICourseRepository repository)
            : base(repository)
        {
        }

        public async Task<ISet<Course>> GetByIdsAsync(IEnumerable<long> ids, CancellationToken cancellationToken)
        {
            ISet<Course> courses = new HashSet<Course>();
            foreach (long id in ids)
            {
                Course course = await GetByIdAsync(id, cancellationToken);
                courses.Add(course);
            }

            return courses;
        }
    }
}
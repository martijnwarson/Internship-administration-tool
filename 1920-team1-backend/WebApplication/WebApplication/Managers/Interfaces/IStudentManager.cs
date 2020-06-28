using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication.Models;

namespace WebApplication.Managers.Interfaces
{
    /// <inheritdoc cref="IPersonManager{TModel}"/>
    public interface IStudentManager
        : IPersonManager<Student>
    {
        /// <summary>
        ///     Create a user for given student <see cref="Student"/>.
        ///     Generating a password for the student"/>.
        /// </summary>
        /// <param name="student">the given <see cref="Student"/></param>
        /// <param name="cancellationToken"> token to cancel the request</param>
        /// <returns>a <see cref="Task"/> returning username and password</returns>
        Task<(string username, string password)> CreateStudentAsync(Student student, CancellationToken cancellationToken);

        /// <summary>
        ///     Toggle the given <see cref="Student"/> as a Favorite for the given <see cref="Student"/>
        /// </summary>
        /// <param name="studentId">the id of the <see cref="Student"/></param>
        /// <param name="internship">The given <see cref="Internship"/></param>
        /// /// <param name="cancellationToken"> token to cancel the request</param>
        /// <returns>a <see cref="Student"/></returns>
        Task<Student> ToggleFavorite(long studentId, Internship internship, CancellationToken cancellationToken);

        Task CreateStudentsFromCsv(IFormFile file, CancellationToken cancellationToken);
    }
}
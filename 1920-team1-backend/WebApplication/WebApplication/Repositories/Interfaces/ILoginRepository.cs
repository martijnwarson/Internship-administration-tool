using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        /// <summary>
        ///     This will verify if the person is allowed to enter the application and gives the claims (ex. roles)
        /// </summary>
        /// <param name="username">The username of the person, in this case the emailAddress</param>
        /// <param name="password">The password in plain text, not the hashed version</param>
        /// <returns>If the person is authorized: the JWT with the claims, else the message he/she isn't allowed to enter the application</returns>
        Task<Person> Login(string username, string password);
    }
}

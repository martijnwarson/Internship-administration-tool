using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Helpers;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="IPersonManager{TModel}" />
    public class PersonManager<TModel>
        : ModelManager<TModel>,
            IPersonManager<TModel>
        where TModel : Person
    {
        public PersonManager(
            IPersonRepository<TModel> repository,
            IPasswordGenerator passwordGenerator,
            IEmailManager emailManager)
            : base(repository)
        {
            PasswordGenerator = passwordGenerator;
            EmailManager = emailManager;
        }

        private IPasswordGenerator PasswordGenerator { get; }
        private IEmailManager EmailManager { get; }

        public async Task<string> CreateLoginAsync(TModel person, CancellationToken cancellationToken)
        {
            string password = PasswordGenerator.CreateRandomPassword();
            using (var hmac = new HMACSHA512())
            {
                person.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                person.PasswordSalt = hmac.Key;
            }

            await EmailManager.SendEmailLogin(person, password, cancellationToken);

            await UpdateAsync(person, cancellationToken);

            return password;
        }

        public async Task CreateLoginsForPersonsAsync(IEnumerable<TModel> persons, CancellationToken cancellationToken)
        {
            foreach (var person in persons)
            {
                await CreateLoginAsync(person, cancellationToken);
            }
        }
    }
}

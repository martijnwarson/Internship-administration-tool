using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DataContext _context;

        public LoginRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Person> Login(string username, string password)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.Email == username);

            if (person == null)
                return null;

            if (!VerifyPasswordHash(password, person.PasswordHash, person.PasswordSalt))
                return null;

            return person;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }

                return true;
            }
        }
    }
}

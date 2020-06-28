using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    public class LoginManager : ILoginManager
    {
        private readonly ILoginRepository _repository;
        private readonly IConfiguration _config;

        public LoginManager(ILoginRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<(Person person, string token)> Login(string username, string password)
        {
            var personFromRepo = await _repository.Login(username, password);

            if (personFromRepo == null)
                return (null, null);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, personFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Email, personFromRepo.Email),
                new Claim(ClaimTypes.Role, personFromRepo.Role.ToString()),
                new Claim(ClaimTypes.GivenName, personFromRepo.FirstName.ToString()),
                new Claim(ClaimTypes.Name, personFromRepo.Name.ToString()), 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenCreation = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokenCreation);

            return (personFromRepo, token);
        }
    }
}

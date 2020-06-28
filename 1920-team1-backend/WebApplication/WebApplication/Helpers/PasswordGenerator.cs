using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Helpers
{
    /// <inheritdoc cref="IPasswordGenerator"/>
    public class PasswordGenerator
        : IPasswordGenerator
    {
        public IConfiguration Configuration { get; }

        public PasswordGenerator(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string CreateRandomPassword()
        {
            StringBuilder password = new StringBuilder();  
            Random random = new Random(); 
            
            char letter;
            for (int i = 0; i < int.Parse(Configuration.GetSection("AppSettings:PasswordLength").Value); i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                password.Append(letter);  
            }

            return password.ToString();
        }
    }
}
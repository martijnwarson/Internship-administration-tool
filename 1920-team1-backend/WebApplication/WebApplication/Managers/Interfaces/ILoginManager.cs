using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Managers.Interfaces
{
    public interface ILoginManager
    {
        Task<(Person person,string token)> Login(string username, string password);
    }
}

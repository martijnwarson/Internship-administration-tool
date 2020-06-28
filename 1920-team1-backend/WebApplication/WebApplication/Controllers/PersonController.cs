using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly object _personManager;

        public PersonController(
            ILogger<CompanyController> logger,
            IPersonManager<Person> personManager)
        {
            _logger = logger;
            _personManager = personManager;
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Dtos;
using WebApplication.Managers.Interfaces;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginManager _loginManager;

        public LoginController(
            ILogger<LoginController> logger,
            ILoginManager loginManager)
        {
            _logger = logger;
            _loginManager = loginManager;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(PersonForLoginDto personForLoginDto)
        {
            var personToken = await _loginManager.Login(personForLoginDto.Username.ToString(),
                personForLoginDto.Password.ToString());
            if (personToken.person == null)
            {
                return Unauthorized();
            }
            _logger.Log(LogLevel.Information, $"{personToken.person.Name} has logged in");

            return Ok(personToken.token);
        }
    }
}
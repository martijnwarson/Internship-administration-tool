using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        public ErrorController(ILogger logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; }

        [Route("/error")]
        public IActionResult Error(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            Logger.Log(LogLevel.Error, context.Error.InnerException, context.Error.StackTrace);
            
            return BadRequest(context.Error.Message);
        }
    }
}
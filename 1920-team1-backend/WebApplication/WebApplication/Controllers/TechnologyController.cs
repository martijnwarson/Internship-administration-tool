using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Dtos;
using WebApplication.Enums;
using WebApplication.Managers.Interfaces;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TechnologyController : ControllerBase
    {
                
        private ILogger<TechnologyController> Logger { get; }
        public ITechnologyManager TechnologyManager { get; }
        public ITechnologyMapper TechnologyMapper { get; }

        public TechnologyController(
            ILogger<TechnologyController> logger,
            ITechnologyManager technologyManager,
            ITechnologyMapper technologyMapper)
        {
            Logger = logger;
            TechnologyManager = technologyManager;
            TechnologyMapper = technologyMapper;
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("all")]
        public async Task<IActionResult> GetAllTechnologies(CancellationToken cancellationToken)
        {
            ISet<Technology> technologies = await TechnologyManager.GetAllAsync(cancellationToken);
            return Ok(technologies.ToArray());
        }
        
        [HttpPost]
        [Route("")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> CreateTechnology([FromBody]TechnologyDto technology, CancellationToken cancellationToken)
        {
            if (technology == null || technology.Id != null)
            {
                return BadRequest();
            }
            
            await TechnologyManager.AddAsync(TechnologyMapper.Map(technology), cancellationToken);
            return Ok();
        }
    }
}
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
    public class PeriodController : ControllerBase
    {
                
        private ILogger<PeriodController> Logger { get; }
        private IPeriodManager PeriodManager { get; }
        private IPeriodMapper PeriodMapper { get; }

        public PeriodController(
            ILogger<PeriodController> logger,
            IPeriodManager periodManager,
            IPeriodMapper periodMapper)
        {
            Logger = logger;
            PeriodManager = periodManager;
            PeriodMapper = periodMapper;
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("all")]
        public async Task<IActionResult> GetAllPeriods(CancellationToken cancellationToken)
        {
            ISet<Period> periods = await PeriodManager.GetAllAsync(cancellationToken);
            return Ok(periods.ToArray());
        }
        
        [HttpPost]
        [Route("")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> CreatePeriod([FromBody]PeriodDto period, CancellationToken cancellationToken)
        {
            if (period == null || period.Id != null)
            {
                return BadRequest();
            }
            
            await PeriodManager.AddAsync(PeriodMapper.Map(period), cancellationToken);
            return Ok();
        }
    }
}
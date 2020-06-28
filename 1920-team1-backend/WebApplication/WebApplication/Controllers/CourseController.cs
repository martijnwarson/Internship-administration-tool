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
    public class CourseController : ControllerBase
    {
        private ILogger<CourseController> Logger { get; }
        public ICourseManager CourseManager { get; }
        public ICourseMapper CourseMapper { get; }

        public CourseController(
            ILogger<CourseController> logger,
            ICourseManager courseManager,
            ICourseMapper courseMapper)
        {
            Logger = logger;
            CourseManager = courseManager;
            CourseMapper = courseMapper;
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("all")]
        public async Task<IActionResult> GetAllCourses(CancellationToken cancellationToken)
        {
            ISet<Course> courses = await CourseManager.GetAllAsync(cancellationToken);
            return Ok(courses.ToArray());
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> CreateCourse([FromBody]CourseDto course, CancellationToken cancellationToken)
        {
            if (course == null || course.Id != null)
            {
                return BadRequest();
            }
            
            await CourseManager.AddAsync(CourseMapper.Map(course), cancellationToken);
            return Ok();
        }
    }
}
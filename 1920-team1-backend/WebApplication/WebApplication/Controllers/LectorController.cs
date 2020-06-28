using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class LectorController
     : ControllerBase
    {
        public LectorController(
            ILectorManager<Lector> lectorManager,
            ILectorMapper<Lector,LectorDto> lectorMapper,
            ICourseManager courseManager)
        {
            LectorManager = lectorManager;
            LectorMapper = lectorMapper;
            CourseManager = courseManager;
        }

        private ILectorManager<Lector> LectorManager { get; }

        private ILectorMapper<Lector, LectorDto> LectorMapper { get; }

        private ICourseManager CourseManager { get; }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles =nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> GetAllLectors(CancellationToken cancellationToken)
        {
            ISet<Lector> lectors = await LectorManager.GetAllAsync(cancellationToken);
            return Ok(LectorMapper.Map(lectors).ToArray());
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR)+ "," + nameof(RoleEnum.LECTOR))]
        public async Task<IActionResult> GetLectorById(long id, CancellationToken cancellationToken)
        {
            Lector foundLector = await LectorManager.GetByIdAsync(id, cancellationToken);
            return Ok(LectorMapper.Map(foundLector));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> DeleteLectorById(
            long id,
            CancellationToken cancellationToken)
        {
            Lector foundLector = await LectorManager.GetByIdAsync(id, cancellationToken);
            if (foundLector == null)
            {
                return BadRequest();
            }

            await LectorManager.RemoveAsync(foundLector, cancellationToken);
            return Ok("Successfully removed");
        }

        [HttpPost]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        [Route("")]
        public async Task<IActionResult> CreateLector(
            [FromBody] LectorDto lector, 
            CancellationToken cancellationToken)
        {
            if (lector == null || lector.Id != null)
            {
                return BadRequest();
            }
            
            Lector mappedLector = LectorMapper.Map(lector);
            
            foreach (var courseId in lector.CoursesIds)
            {
                Course foundCourse = await CourseManager.GetByIdAsync(courseId, cancellationToken);
                if (foundCourse == null)
                {
                    return BadRequest();
                }
                mappedLector.Courses.Add(new LectorCourse(mappedLector, foundCourse));
            }

            var result =
                await LectorManager
                    .CreateLectorAsync(
                        mappedLector, 
                        cancellationToken);

            return Ok("Lector successfully created");
        }

        [HttpPut]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        [Route("{id}")]
        public async Task<IActionResult> UpdateLector(
            long id, 
            [FromBody] LectorDto lector, 
            CancellationToken cancellationToken)
        {
            if(lector == null || lector.Id != id)
            {
                return BadRequest();
            }

            Lector foundlector = await LectorManager.GetByIdAsync(id, cancellationToken);

            LectorMapper.OnMap(lector, foundlector);
            await LectorManager.UpdateAsync(foundlector, cancellationToken);

            return Ok("Succesfully updated");
        }


        
    }
}
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : 
        ControllerBase
    {
        private ILogger<StudentController> Logger { get; }
        private IStudentManager StudentManager { get; }
        private IStudentMapper StudentMapper { get; }
        private IStudentOverviewMapper StudentOverviewMapper { get; }
        public IStudentListMapper StudentListMapper { get; }
        public ICourseManager CourseManager { get; }

        public StudentController(
            ILogger<StudentController> logger, 
            IStudentManager studentManager,
            IStudentMapper studentMapper,
            IStudentOverviewMapper studentOverviewMapper,
            IStudentListMapper studentListMapper,
            ICourseManager courseManager)
        {
            Logger = logger;
            StudentManager = studentManager;
            StudentMapper = studentMapper;
            StudentOverviewMapper = studentOverviewMapper;
            StudentListMapper = studentListMapper;
            CourseManager = courseManager;
        }
        
        [HttpPost]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        [Route("")]
        public async Task<IActionResult> CreateStudent(
            [FromBody] StudentDto student,
            CancellationToken cancellationToken)
        {
            if (student == null || student.Id != null)
            {
                return BadRequest();
            }

            Course course = await CourseManager.GetByIdAsync(student.CourseId, cancellationToken);
            if (course == null)
            {
                return BadRequest();
            }
            
            Student mappedStudent = StudentMapper.Map(student);
            mappedStudent.Course = course;
            
            var result = await StudentManager
                .CreateStudentAsync(
                    mappedStudent,
                    cancellationToken);

            return Ok("Student successfully created");
        }

        [HttpPost]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        [Route("upload")]
        public async Task<IActionResult> CreateStudentsFromCsv(
            [FromForm] IFormFile file,
            CancellationToken cancellationToken)
        {
            if (file.Length > 2097152)
            {
                return StatusCode((int) HttpStatusCode.RequestEntityTooLarge);
            }

            await StudentManager.CreateStudentsFromCsv(file, cancellationToken);
            return Ok("Students successfully created");
        }

        [HttpGet]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        [Route("all")]
        public async Task<IActionResult> GetAllStudents(CancellationToken cancellationToken)
        {
            ISet<Student> foundStudents = await StudentManager.GetAllAsync(cancellationToken);
            return Ok(StudentListMapper.Map(foundStudents));
        }

        [HttpGet]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        [Route("{id}")]
        public async Task<IActionResult> GetStudentById(
            long id,
            CancellationToken cancellationToken)
        {
            Student foundStudent = await StudentManager
                .GetByIdAsync(
                    id,
                    cancellationToken);

            return Ok(StudentOverviewMapper.Map(foundStudent));
        }
        
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> DeleteStudentById(
            long id,
            CancellationToken cancellationToken)
        {
            Student foundStudent = await StudentManager.GetByIdAsync(id, cancellationToken);
            if (foundStudent == null)
            {
                return BadRequest();
            }

            await StudentManager.RemoveAsync(foundStudent, cancellationToken);
            return Ok("Successfully removed");
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> UpdateStudent(
            long id, 
            [FromBody] StudentDto student,
            CancellationToken cancellationToken)
        {
            if (student == null || student.Id != id)
            {
                return BadRequest();
            }

            Student foundStudent = await StudentManager.GetByIdAsync(id, cancellationToken);

            StudentMapper.OnMap(student, foundStudent);
            await StudentManager.UpdateAsync(foundStudent, cancellationToken);

            return Ok("Succesfully updated");
        }

    }
}
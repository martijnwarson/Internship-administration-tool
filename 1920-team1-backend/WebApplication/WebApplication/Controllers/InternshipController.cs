using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Dtos;
using WebApplication.Enums;
using WebApplication.Helpers;
using WebApplication.Managers.Interfaces;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;
using Internship = WebApplication.Models.Internship;

namespace WebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InternshipController : ControllerBase
    {
        private ILogger<InternshipController> Logger { get; }
        private IInternshipManager InternshipManager { get; }
        private IInternshipMapper InternshipMapper { get; }
        private IPeriodManager PeriodManager { get; }
        private ITechnologyManager TechnologyManager { get; }
        private ICourseManager CourseManager { get; }
        private IStudentManager StudentManager { get; }
        private IValidationManager ValidationManager { get; }
        private IPersonMapper<Person, PersonDto> PersonMapper { get; }
        private IPersonManager<Person> PersonManager { get; }
        private IUserVerifier UserVerifier { get; }
        private IInternshipOverviewMapper InternshipOverviewMapper { get; }
        private IInternshipListMapper InternshipListMapper { get; }
        public IInternshipListStudentMapper InternshipListStudentMapper { get; }
        private IEmailManager EmailManager { get; }

        public InternshipController(
            ILogger<InternshipController> logger,
            IInternshipManager internshipManager,
            IInternshipMapper internshipMapper,
            IPeriodManager periodManager,
            ITechnologyManager technologyManager,
            ICourseManager courseManager,
            IStudentManager studentManager,
            IValidationManager validationManager,
            IPersonMapper<Person, PersonDto> personMapper,
            IPersonManager<Person> personManager,
            IUserVerifier userVerifier,
            IInternshipOverviewMapper internshipOverviewMapper,
            IInternshipListMapper internshipListMapper,
            IInternshipListStudentMapper internshipListStudentMapper,
            IEmailManager emailManager)
        {
            Logger = logger;
            InternshipManager = internshipManager;
            InternshipMapper = internshipMapper;
            PeriodManager = periodManager;
            TechnologyManager = technologyManager;
            CourseManager = courseManager;
            StudentManager = studentManager;
            ValidationManager = validationManager;
            PersonMapper = personMapper;
            PersonManager = personManager;
            UserVerifier = userVerifier;
            InternshipOverviewMapper = internshipOverviewMapper;
            InternshipListMapper = internshipListMapper;
            InternshipListStudentMapper = internshipListStudentMapper;
            EmailManager = emailManager;
        }
        
        private long UserId 
            => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetInternship(long id, CancellationToken cancellationToken)
        {
            Internship foundInternship = await InternshipManager.GetByIdAsync(id, cancellationToken);
            if (!await UserVerifier.VerifyUserAsync(UserId, foundInternship, cancellationToken))
            {
                return Forbid();
            }

            return Ok(InternshipOverviewMapper.Map(foundInternship));
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> GetAllInternships(CancellationToken cancellationToken)
        {
            ISet<Internship> foundInternships = await InternshipManager.GetAllAsync(cancellationToken);
            return Ok(InternshipListMapper.Map(foundInternships));
        }
        
        [HttpGet]
        [Route("lector")]
        [Authorize(Roles = nameof(RoleEnum.LECTOR)+","+nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> GetAllInternshipsByLectorId(CancellationToken cancellationToken)
        {
            ISet<Internship> foundInternships = await InternshipManager.GetAllInternshipsByLectorId(UserId, cancellationToken);
            return Ok(InternshipListMapper.Map(foundInternships));
        }
        
        [HttpGet]
        [Route("student")]
        [Authorize(Roles = nameof(RoleEnum.STUDENT))]
        public async Task<IActionResult> GetAllInternshipsForStudentOverview(CancellationToken cancellationToken)
        {
            ISet<Internship> foundInternships = await InternshipManager.GetAllInternshipsForStudents(cancellationToken);
            return Ok(InternshipListStudentMapper.Map(foundInternships));
        }
        
        [HttpGet]
        [Route("company")]
        [Authorize(Roles = nameof(RoleEnum.CONTACT))]
        public async Task<IActionResult> GetAllInternshipsByContactId(CancellationToken cancellationToken)
        {
            ISet<Internship> foundInternships =
                await InternshipManager.GetAllInternshipsByContactId(UserId, cancellationToken);
            return Ok(InternshipListMapper.Map(foundInternships));
        }
        
        [HttpGet]
        [Route("company/{id}")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> GetAllInternshipsByCompanyId(
            long id,
            CancellationToken cancellationToken)
        {
            ISet<Internship> foundInternships = await InternshipManager.GetAllInternshipsByCompanyId(id, cancellationToken);
            return Ok(InternshipListMapper.Map(foundInternships));
        }
        
        [HttpGet]
        [Route("state/{state}")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR)+","+nameof(RoleEnum.STUDENT))]
        public async Task<IActionResult> GetAllInternshipsByState(
            InternshipStateEnum state,
            CancellationToken cancellationToken)
        {
            if (!await UserVerifier.VerifyUserAsync(UserId, state, cancellationToken))
            {
                return Forbid();
            }
            ISet<Internship> foundInternships = await InternshipManager.GetAllInternshipsByState(state, cancellationToken);
            return Ok(InternshipListMapper.Map(foundInternships));
        }
        
        [HttpGet]
        [Route("favorite")]
        [Authorize(Roles = nameof(RoleEnum.STUDENT))]
        public async Task<IActionResult> GetAllFavorites(
            CancellationToken cancellationToken)
        {
            Student loggedInStudent = await StudentManager.GetByIdAsync(UserId, cancellationToken);
            var favorites = loggedInStudent.Favorites.Select(si => si.Internship).ToList();
            return Ok(InternshipListMapper.Map(favorites));
        }
        
        [HttpGet]
        [Route("{id}/pdf")]
        public async Task<IActionResult> GetInternshipPdf(long id, CancellationToken cancellationToken)
        {
            Internship foundInternship = await InternshipManager.GetByIdAsync(id, cancellationToken);
            if (!await UserVerifier.VerifyUserAsync(UserId, foundInternship, cancellationToken))
            {
                return Forbid();
            }

            return InternshipManager.GetInternshipPdf(foundInternship);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = nameof(RoleEnum.CONTACT))]
        public async Task<IActionResult> CreateInternship(
            [FromBody] InternshipDto internship,
            CancellationToken cancellationToken)
        {
            if (internship == null || internship.Id != null)
            {
                return BadRequest();
            }

            Person loggedInPerson = await PersonManager.GetByIdAsync(UserId, cancellationToken);
            Company company = await UserVerifier.VerifyUserCompanyAsync(loggedInPerson, cancellationToken);

            Internship mappedInternship = InternshipMapper.Map(internship);
            if (company == null || company.State != CompanyStateEnum.ACTIVE)
            {
                return Forbid();
            }

            mappedInternship.Company = company;
            foreach (long courseId in internship.CourseIds)
            {
                Course course = await CourseManager.GetByIdAsync(courseId, cancellationToken);
                if (course == null)
                {
                    return BadRequest();
                }
                
                mappedInternship.Courses.Add(new InternshipCourse(mappedInternship, course));
            }
            foreach (long technologyId in internship.TechnologyIds)
            {
                Technology technology = await TechnologyManager.GetByIdAsync(technologyId, cancellationToken);
                if (technology == null)
                {
                    return BadRequest();
                }
                
                mappedInternship.Technologies.Add(new InternshipTechnology(mappedInternship, technology));
            }
            foreach (long periodId in internship.PeriodIds)
            {
                Period period = await PeriodManager.GetByIdAsync(periodId, cancellationToken);
                if (period == null)
                {
                    return BadRequest();
                }
                
                mappedInternship.Periods.Add(new InternshipPeriod(mappedInternship, period));
            }

            if (internship.StudentIds != null)
            {
                foreach (long studentId in internship.StudentIds)
                {
                    Student student = await StudentManager.GetByIdAsync(studentId, cancellationToken);
                    if (student == null)
                    {
                        return BadRequest();
                    }

                    mappedInternship.Students.Add(new InternshipStudent(mappedInternship, student));
                }
            }

            foreach (PersonDto promotor in internship.Promotors)
            {
                Person person = PersonMapper.Map(promotor);
                mappedInternship.Promotors.Add(new InternshipPerson(mappedInternship, person));
            }

            mappedInternship.State = InternshipStateEnum.NEW;

            await InternshipManager
                .AddAsync(
                    mappedInternship,
                    cancellationToken);

            return Ok("Successfully saved");
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = nameof(RoleEnum.CONTACT))]
        public async Task<IActionResult> UpdateInternship(
            long id,
            [FromBody] InternshipDto internship,
            CancellationToken cancellationToken)
        {
            if (internship == null || internship.Id != id)
            {
                return BadRequest();
            }
            
            Internship foundInternship = await InternshipManager.GetByIdAsync(id, cancellationToken);

            if (foundInternship.State != InternshipStateEnum.NEW && foundInternship.State != InternshipStateEnum.TO_BE_MODIFIED)
            {
                return BadRequest("state of the internship does not permit changes");
            }

            if (!await UserVerifier.VerifyUserAsync(UserId, foundInternship, cancellationToken))
            {
                return Forbid();
            }

            if (foundInternship.State == InternshipStateEnum.TO_BE_MODIFIED)
            {
                foundInternship.State = InternshipStateEnum.MODIFIED;
                ISet<Lector> lectors = foundInternship.Validations.Select(v => v.Lector).ToHashSet();
                await ValidationManager.CreateValidationsByInternshipAndLectorsAsync(foundInternship, lectors, cancellationToken);
            }
            
            InternshipMapper.OnMap(internship, foundInternship);
            await InternshipManager.UpdateAsync(foundInternship, cancellationToken);
            return Ok("Successfully updated");
        }
        
        [HttpPut]
        [Route("{id}/state")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> UpdateInternshipState(
            long id,
            [FromBody] InternShipUpdateStateDto dto,
            CancellationToken cancellationToken)
        {
            if (dto== null)
            {
                return BadRequest();
            }
            
            Internship foundInternship = await InternshipManager.GetByIdAsync(id, cancellationToken);

            if (foundInternship == null)
            {
                return NotFound();
            }

            if (foundInternship.State != InternshipStateEnum.MODIFIED &&
                foundInternship.State != InternshipStateEnum.PENDING)
            {
                return BadRequest("Not allowed to change the state of the internship to this state");
            }

            if (dto.FeedBack != null)
            {
                foundInternship.FeedBack = new FeedBack(dto.FeedBack.Value, dto.FeedBack.ModifiedAt);
            }

            switch (dto.State)
            {
                case InternshipStateEnum.TO_BE_MODIFIED:
                    await EmailManager.SendEmailInternshipFeedback(foundInternship, cancellationToken);
                    break;
                case InternshipStateEnum.REJECTED:
                    await EmailManager.SendEmailInternshipDeny(foundInternship, cancellationToken);
                    break;
                case InternshipStateEnum.APPROVED:
                    await EmailManager.SendEmailInternshipApprove(foundInternship, cancellationToken);
                    break;
                default:
                    return BadRequest("Not allowed to change the state of the internship to this state");
            }

            foundInternship.State = dto.State;
            await InternshipManager.UpdateAsync(foundInternship, cancellationToken);
            return Ok("Successfully updated");
        }

        [HttpPost]
        [Route("{id}/favorite")]
        [Authorize(Roles = nameof(RoleEnum.STUDENT))]
        public async Task<IActionResult> MakeFavorite(long id, CancellationToken cancellationToken)
        {
            Internship foundInternship = await InternshipManager.GetByIdAsync(id, cancellationToken);
            if (foundInternship ==null || foundInternship.State != InternshipStateEnum.APPROVED)
            {
                return NotFound();
            }

            Student student = await StudentManager.ToggleFavorite(UserId, foundInternship, cancellationToken);
            return Ok();
        }
    }
}
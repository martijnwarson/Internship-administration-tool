using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dtos;
using WebApplication.Enums;
using WebApplication.Helpers;
using WebApplication.Managers.Interfaces;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/internship/{internshipId}/[controller]")]
    public class ValidationController : ControllerBase
    {
        public ValidationController(
            IInternshipManager internshipManager,
            IUserVerifier userVerifier,
            IValidationOverviewMapper validationOverviewMapper,
            ILectorManager<Lector> lectorManager,
            IValidationManager validationManager,
            IValidationUpdateMapper validationUpdateMapper)
        {
            InternshipManager = internshipManager;
            UserVerifier = userVerifier;
            ValidationOverviewMapper = validationOverviewMapper;
            LectorManager = lectorManager;
            ValidationManager = validationManager;
            ValidationUpdateMapper = validationUpdateMapper;
        }

        private IInternshipManager InternshipManager { get; }

        private IUserVerifier UserVerifier { get; }

        private IValidationOverviewMapper ValidationOverviewMapper { get; }
        
        private ILectorManager<Lector> LectorManager { get; }
        
        private IValidationManager ValidationManager { get; }
        
        private IValidationUpdateMapper ValidationUpdateMapper { get; }

        private long UserId 
            => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetValidation(long internshipId, long id, CancellationToken cancellationToken)
        {
            Internship foundInternship = await InternshipManager.GetByIdAsync(internshipId, cancellationToken);
            if (foundInternship == null)
            {
                return BadRequest();
            }
            
            if (!await UserVerifier.VerifyUserAsync(UserId, foundInternship, cancellationToken))
            {
                return Forbid();
            }

            Validation foundValidation = foundInternship.Validations.SingleOrDefault(v => v.Id == id);
            if (foundValidation == null)
            {
                return NotFound();
            }

            if (!await UserVerifier.VerifyUserAsync(UserId, foundValidation, cancellationToken))
            {
                return Forbid();
            }

            return Ok(ValidationOverviewMapper.Map(foundValidation));
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> GetAllValidations(long internshipId, CancellationToken cancellationToken)
        {
            Internship foundInternship = await InternshipManager.GetByIdAsync(internshipId, cancellationToken);
            if (foundInternship == null)
            {
                return NotFound();
            }
            return Ok(ValidationOverviewMapper.Map(foundInternship.Validations));
        }
        
        [HttpGet]
        [Route("lector")]
        [Authorize(Roles = nameof(RoleEnum.LECTOR)+","+nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> GetAllValidationsByLectorId(long internshipId, CancellationToken cancellationToken)
        {
            Internship foundInternship = await InternshipManager.GetByIdAsync(internshipId, cancellationToken);
            if (foundInternship == null)
            {
                return NotFound();
            }
            
            if (!await UserVerifier.VerifyUserAsync(UserId, foundInternship, cancellationToken))
            {
                return Forbid();
            }
            
            return Ok(
                ValidationOverviewMapper
                    .Map(
                        foundInternship
                            .Validations
                            .Where(v => v.Lector.Id == UserId))
                    .OrderByDescending(v => v.Date)
                    .ToArray());
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> CreateValidations(
            long internshipId,
            [FromBody] ValidationCreateDto validationCreateDto,
            CancellationToken cancellationToken)
        {
            Internship foundInternship = await InternshipManager.GetByIdAsync(internshipId, cancellationToken);
            if (foundInternship == null)
            {
                return NotFound();
            }

            ISet<Lector> lectors = await LectorManager.GetLectorsByIdsAsync(validationCreateDto.LectorIds, cancellationToken);
            await ValidationManager
                .CreateValidationsByInternshipAndLectorsAsync(
                foundInternship,
                lectors,
                cancellationToken);
            foundInternship.State = InternshipStateEnum.PENDING;
            
            await InternshipManager.UpdateAsync(foundInternship, cancellationToken);
            
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = nameof(RoleEnum.LECTOR) + "," + nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> UpdateValidation(
            long internshipId,
            long id,
            [FromBody] ValidationUpdateDto dto,
            CancellationToken cancellationToken)
        {
            if (dto == null || dto.Id != id)
            {
                return BadRequest();
            }
            
            Internship foundInternship = await InternshipManager.GetByIdAsync(internshipId, cancellationToken);
            if (foundInternship == null)
            {
                return NotFound();
            }

            if (!await UserVerifier.VerifyUserAsync(UserId, foundInternship, cancellationToken))
            {
                return Forbid();
            }

            Validation toUpdateValidation = foundInternship.Validations.SingleOrDefault(v => v.Id == id);
            if (!await UserVerifier.VerifyUserAsync(UserId, toUpdateValidation, cancellationToken))
            {
                return Forbid();
            }
            
            ValidationUpdateMapper.OnMap(dto, toUpdateValidation);
            await ValidationManager.UpdateAsync(toUpdateValidation, cancellationToken);
            return Ok("Updated");
        }
    }
}
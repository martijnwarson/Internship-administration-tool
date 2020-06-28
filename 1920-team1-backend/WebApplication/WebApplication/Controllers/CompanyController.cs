using System;
using System.Collections.Generic;
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

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public CompanyController(
            ILogger<InternshipController> logger,
            ICompanyManager companyManager,
            ICompanyMapper companyMapper,
            IUpdateStateCompanyMapper updateStateCompanyMapper,
            ICompanyCreateMapper companyCreateMapper,
            IUserVerifier userVerifier)
        {
            Logger = logger;
            CompanyManager = companyManager;
            CompanyMapper = companyMapper;
            UpdateStateCompanyMapper = updateStateCompanyMapper;
            CompanyCreateMapper = companyCreateMapper;
            UserVerifier = userVerifier;
        }

        private ILogger<InternshipController> Logger { get; }
        private ICompanyManager CompanyManager { get; }
        private ICompanyMapper CompanyMapper { get; }
        private IUpdateStateCompanyMapper UpdateStateCompanyMapper { get; }
        private ICompanyCreateMapper CompanyCreateMapper { get; }
        private IUserVerifier UserVerifier { get; }
        private long UserId 
            => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> CreateCompany(
            [FromBody] CompanyCreateDto company,
            CancellationToken cancellationToken)
        {
            if (company == null || company.Id != null)
            {
                return BadRequest();
            }

            Company mappedCompany = CompanyCreateMapper.Map(company);
            mappedCompany.State = CompanyStateEnum.NEW;
            
            await CompanyManager
                .AddAsync(
                    mappedCompany,
                    cancellationToken);

            return Ok("Successfully saved");
        }
        
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR)+","+nameof(RoleEnum.CONTACT))]
        public async Task<IActionResult> GetCompany(
            long id,
            CancellationToken cancellationToken)
        {
            Company foundCompany = await CompanyManager.GetByIdAsync(id, cancellationToken);
            if (!await UserVerifier.VerifyUserAsync(UserId, foundCompany, cancellationToken))
            {
                return Forbid();
            }
            
            return Ok(CompanyMapper.Map(foundCompany));
        }
        
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> GetAllCompanies(CancellationToken cancellationToken)
        {
            ISet<Company> foundCompanies = await CompanyManager.GetAllAsync(cancellationToken);
            return Ok(CompanyMapper.Map(foundCompanies));
        }
        
        [HttpGet]
        [Route("all/{state}")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> GetAllCompaniesByState(
            CompanyStateEnum state,
            CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(CompanyStateEnum), state))
            {
                return BadRequest();
            }
            
            ISet<Company> foundCompanies = await CompanyManager.GetAllByStateAsync(state, cancellationToken);
            return Ok(CompanyMapper.Map(foundCompanies));
        }

        [HttpGet]
        [Route("pdf")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPDF(
            CancellationToken cancellationToken)
        {
            FileStreamResult streamResult = await CompanyManager.GetCompanyPdfAsync(UserId, cancellationToken);
            return streamResult;
        }
        
        [HttpPut]
        [Route("{id}/state")]
        [Authorize(Roles = nameof(RoleEnum.COÖRDINATOR))]
        public async Task<IActionResult> UpdateState(
            long id,
            [FromBody] UpdateStateCompanyDto updateState,
            CancellationToken cancellationToken)
        {
            Company foundCompany = await CompanyManager.GetByIdAsync(id, cancellationToken);
            if (foundCompany == null)
            {
                return NotFound();
            }

            if (updateState.FeedBack != null)
            {
                foundCompany.FeedBack = new FeedBack(updateState.FeedBack.Value, updateState.FeedBack.ModifiedAt);
            }
            
            if (updateState.CompanyState == CompanyStateEnum.ACTIVE)
            {
                if (foundCompany.State == CompanyStateEnum.ACTIVE)
                {
                    BadRequest("Company is already active");
                }

                var result = await CompanyManager.ActivateCompanyAsync(foundCompany, cancellationToken);
                
                return Ok("Company successfully activated");
            }
            
            if (foundCompany.State == CompanyStateEnum.INACTIVE)
            {
                BadRequest("Company is already Inactive");
            }

            UpdateStateCompanyMapper.OnMap(updateState, foundCompany);
            await CompanyManager.DeactivateCompanyAsync(foundCompany, cancellationToken);
            return Ok("Deactivated");
        }
    }
}
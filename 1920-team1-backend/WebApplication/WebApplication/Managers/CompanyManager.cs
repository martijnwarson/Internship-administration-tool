using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Enums;
using WebApplication.Helpers;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="ICompanyManager"/>
    public class CompanyManager
        : ModelManager<Company>,
            ICompanyManager
    {
        public CompanyManager(
            ICompanyRepository repository,
            IPersonManager<Person> personManager,
            IEmailManager emailManager,
            IPdfGenerator pdfGenerator)
            : base(repository)
        {
            CompanyRepository = repository;
            PersonManager = personManager;
            EmailManager = emailManager;
            PdfGenerator = pdfGenerator;
        }

        private ICompanyRepository CompanyRepository { get; }

        public IPersonManager<Person> PersonManager { get; }
        
        private IEmailManager EmailManager { get; }
        
        public IPdfGenerator PdfGenerator { get; }

        public async Task<Company> GetCompanyByContactAsync(Person contactPerson, CancellationToken cancellationToken)
            => await CompanyRepository.GetCompanyByContactAsync(contactPerson, cancellationToken);

        public async Task<ISet<Company>> GetAllByStateAsync(CompanyStateEnum stateEnum, CancellationToken cancellationToken)
        {
            IList<Company> companies = await CompanyRepository.GetAllByStateAsync(stateEnum, cancellationToken);
            return companies.ToHashSet();
        }

        public async Task<(string username, string password)> ActivateCompanyAsync(Company company, CancellationToken cancellationToken)
        {
            company.State = CompanyStateEnum.ACTIVE;
            string password = await PersonManager.CreateLoginAsync(company.ContactPerson, cancellationToken);
            await CompanyRepository.UpdateAsync(company, cancellationToken);
            await EmailManager.SendEmailCompanyApprove(company, cancellationToken);
            return (company.ContactPerson.Email, password);
        }

        public async Task DeactivateCompanyAsync(Company company, CancellationToken cancellationToken)
        {
            company.State = CompanyStateEnum.INACTIVE;
            await CompanyRepository.UpdateAsync(company, cancellationToken);
            await EmailManager.SendEmailCompanyDeny(company, cancellationToken);
        }

        public async Task<FileStreamResult> GetCompanyPdfAsync(long userId, CancellationToken cancellationToken)
        {
            Person contactPerson = await PersonManager.GetByIdAsync(userId, cancellationToken);
            Company foundCompany = await CompanyRepository.GetCompanyByContactAsync(contactPerson, cancellationToken);
            FileStreamResult stream = PdfGenerator.CreateCompanyPdf(foundCompany);
            return stream;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Enums;
using WebApplication.Helpers;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="IInternshipManager"/>
    public class InternshipManager
        : ModelManager<Internship>,
            IInternshipManager
    {
        public InternshipManager(
            IInternshipRepository repository,
            IPdfGenerator pdfGenerator)
            : base(repository)
        {
            InternshipRepository = repository;
            PdfGenerator = pdfGenerator;
        }

        private IInternshipRepository InternshipRepository { get; }
        public IPdfGenerator PdfGenerator { get; }

        public override Task AddAsync(Internship entity, CancellationToken cancellationToken)
        {
            if (entity.Address == null)
            {
                entity.Address = entity.Company.Address;
            }
            
            return base.AddAsync(entity, cancellationToken);
        }

        public async Task<ISet<Internship>> GetAllInternshipsByLectorId(long lectorId, CancellationToken cancellationToken)
        {
            IList<Internship> internships = await InternshipRepository.GetAllInternshipsByLectorId(lectorId, cancellationToken);
            return internships.ToHashSet();
        }
        
        public async Task<ISet<Internship>> GetAllInternshipsByContactId(long contactId, CancellationToken cancellationToken)
        {
            IList<Internship> internships = await InternshipRepository.GetAllInternshipsByContactId(contactId, cancellationToken);
            return internships.ToHashSet();
        }

        public async Task<ISet<Internship>> GetAllInternshipsByCompanyId(long companyId, CancellationToken cancellationToken)
        {
            IList<Internship> internships = await InternshipRepository.GetAllInternshipsByCompanyId(companyId, cancellationToken);
            return internships.ToHashSet();
        }

        public async Task<ISet<Internship>> GetAllInternshipsByState(InternshipStateEnum stateEnum, CancellationToken cancellationToken)
        {
            IList<Internship> internships = await InternshipRepository.GetAllInternshipsByState(stateEnum, cancellationToken);
            return internships.ToHashSet();
        }

        public async Task<ISet<Internship>> GetAllInternshipsForStudents(CancellationToken cancellationToken)
        {
            IList<Internship> internships =
                await InternshipRepository.GetAllInternshipsForStudentOverview(cancellationToken);
            return internships.ToHashSet();
        }
        
        public FileStreamResult GetInternshipPdf(Internship internship)
        {
            FileStreamResult stream = PdfGenerator.CreateInternshipPdf(internship);
            return stream;
        }
    }
}
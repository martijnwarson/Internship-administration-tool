using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Managers.Interfaces
{
    /// <inheritdoc cref="IModelManager{TModel}"/>
    public interface IInternshipManager
        : IModelManager<Internship>
    {
        Task<ISet<Internship>> GetAllInternshipsByLectorId(long lectorId, CancellationToken cancellationToken);
        Task<ISet<Internship>> GetAllInternshipsByContactId(long contactId, CancellationToken cancellationToken);
        Task<ISet<Internship>> GetAllInternshipsByCompanyId(long companyId, CancellationToken cancellationToken);
        Task<ISet<Internship>> GetAllInternshipsByState(InternshipStateEnum stateEnum, CancellationToken cancellationToken);
        Task<ISet<Internship>> GetAllInternshipsForStudents(CancellationToken cancellationToken);
        FileStreamResult GetInternshipPdf(Internship internship);
    }
}
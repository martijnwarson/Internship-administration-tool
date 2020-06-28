using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    /// <inheritdoc cref="ICrudRepository{TModel}" />
    public interface IInternshipRepository
        : ICrudRepository<Internship>
    {
        Task<IList<Internship>> GetAllInternshipsByLectorId(long lectorId, CancellationToken cancellationToken);
        Task<IList<Internship>> GetAllInternshipsByContactId(long contactId, CancellationToken cancellationToken);
        Task<IList<Internship>> GetAllInternshipsByCompanyId(long companyId, CancellationToken cancellationToken);
        Task<IList<Internship>> GetAllInternshipsByState(InternshipStateEnum stateEnum, CancellationToken cancellationToken);
        Task<IList<Internship>> GetAllInternshipsForStudentOverview(CancellationToken cancellationToken);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Enums;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.UnitTests.UnitTests.InMemoryRepositories
{
    public class InMemoryInternshipRepository
        : InMemoryRepository<Internship>,
            IInternshipRepository
    {
        public InMemoryInternshipRepository(IList<Internship> models) : base(models)
        {
        }

        public async Task<IList<Internship>> GetAllInternshipsByLectorId(long lectorId,
            CancellationToken cancellationToken)
            => await Factory.StartNew(() => Models.Where(i => i.Validations.Any(v => v.Lector.Id == lectorId)).ToList(),
                cancellationToken);

        public async Task<IList<Internship>> GetAllInternshipsByContactId(long contactId, CancellationToken cancellationToken)
            => await Factory.StartNew(() => Models.Where(i => i.ContactPerson.Id == contactId).ToList(),
                cancellationToken);

        public async Task<IList<Internship>> GetAllInternshipsByCompanyId(long companyId,
            CancellationToken cancellationToken)
            => await Factory.StartNew(() => Models.Where(i => i.Company.Id == companyId).ToList(), cancellationToken);

        public async Task<IList<Internship>> GetAllInternshipsByState(InternshipStateEnum stateEnum,
            CancellationToken cancellationToken)
            => await Factory.StartNew(() => Models.Where(i => i.State == stateEnum).ToList(), cancellationToken);

        public async Task<IList<Internship>> GetAllInternshipsForStudentOverview(CancellationToken cancellationToken)
            => await Factory.StartNew(
                () => Models.Where(i => i.State == InternshipStateEnum.APPROVED && i.StudentAmount != i.Students.Count)
                    .ToList(), cancellationToken);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Enums;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class InternshipRepository
        : CrudRepository<Internship>,
            IInternshipRepository
    {
        public InternshipRepository(DataContext datacontext)
            : base(datacontext)
        {
        }

        public override async Task<IList<Internship>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await DataContext
                .Set<Internship>()
                .Include(i => i.Company)
                .ToListAsync(cancellationToken);
        }

        public override async Task<Internship> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await DataContext
                .Set<Internship>()
                .Include(i => i.Address)
                .Include(i => i.Company)
                .ThenInclude(c => c.Address)
                .Include(i => i.ContactPerson)
                .Include(i => i.FeedBack)
                .Include(i => i.Courses)
                .ThenInclude(c => c.Course)
                .Include(i => i.Periods)
                .ThenInclude(p => p.Period)
                .Include(i => i.Promotors)
                .ThenInclude(p => p.Person)
                .Include(i => i.Students)
                .ThenInclude(s => s.Student)
                .Include(i => i.Technologies)
                .ThenInclude(t => t.Technology)
                .Include(i => i.Validations)
                .ThenInclude(v => v.Lector)
                .Include(i => i.Validations)
                .ThenInclude(v => v.FeedBack)
                .SingleOrDefaultAsync(i => i.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<IList<Internship>> GetAllInternshipsByLectorId(long lectorId, CancellationToken cancellationToken)
        {
            return await DataContext
                .Set<Internship>()
                .Include(i => i.Address)
                .Include(i => i.Company)
                .ThenInclude(c => c.Address)
                .Include(i => i.ContactPerson)
                .Include(i => i.FeedBack)
                .Include(i => i.Courses)
                .ThenInclude(c => c.Course)
                .Include(i => i.Periods)
                .ThenInclude(p => p.Period)
                .Include(i => i.Promotors)
                .ThenInclude(p => p.Person)
                .Include(i => i.Students)
                .ThenInclude(s => s.Student)
                .Include(i => i.Technologies)
                .ThenInclude(t => t.Technology)
                .Include(i => i.Validations)
                .ThenInclude(v => v.Lector)
                .Include(i => i.Validations)
                .ThenInclude(v => v.FeedBack)
                .Where(i => i.Validations.Any(v => v.Lector.Id == lectorId))
                .ToListAsync(cancellationToken: cancellationToken);
        }
        
        public async Task<IList<Internship>> GetAllInternshipsByContactId(long contactId, CancellationToken cancellationToken)
        {
            return await DataContext
                .Set<Internship>()
                .Include(i => i.Address)
                .Include(i => i.Company)
                .ThenInclude(c => c.Address)
                .Include(i => i.ContactPerson)
                .Include(i => i.FeedBack)
                .Include(i => i.Courses)
                .ThenInclude(c => c.Course)
                .Include(i => i.Periods)
                .ThenInclude(p => p.Period)
                .Include(i => i.Promotors)
                .ThenInclude(p => p.Person)
                .Include(i => i.Students)
                .ThenInclude(s => s.Student)
                .Include(i => i.Technologies)
                .ThenInclude(t => t.Technology)
                .Include(i => i.Validations)
                .ThenInclude(v => v.Lector)
                .Include(i => i.Validations)
                .ThenInclude(v => v.FeedBack)
                .Where(i => i.Company.ContactPerson.Id == contactId)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IList<Internship>> GetAllInternshipsByCompanyId(long companyId, CancellationToken cancellationToken)
        {
            return await DataContext
                .Set<Internship>()
                .Include(i => i.Address)
                .Include(i => i.Company)
                .ThenInclude(c => c.Address)
                .Include(i => i.ContactPerson)
                .Include(i => i.FeedBack)
                .Include(i => i.Courses)
                    .ThenInclude(c => c.Course)
                .Include(i => i.Periods)
                    .ThenInclude(p => p.Period)
                .Include(i => i.Promotors)
                    .ThenInclude(p => p.Person)
                .Include(i => i.Students)
                    .ThenInclude(s => s.Student)
                .Include(i => i.Technologies)
                    .ThenInclude(t => t.Technology)
                .Include(i => i.Validations)
                    .ThenInclude(v => v.Lector)
                .Include(i => i.Validations)
                    .ThenInclude(v => v.FeedBack)
                .Where(i => i.Company.Id == companyId)
                .ToListAsync(cancellationToken: cancellationToken);
        }
        
        public async Task<IList<Internship>> GetAllInternshipsByState(InternshipStateEnum stateEnum, CancellationToken cancellationToken)
        {
            return await DataContext
                .Set<Internship>()
                .Include(i => i.Address)
                .Include(i => i.Company)
                .ThenInclude(c => c.Address)
                .Include(i => i.ContactPerson)
                .Include(i => i.FeedBack)
                .Include(i => i.Courses)
                .ThenInclude(c => c.Course)
                .Include(i => i.Periods)
                .ThenInclude(p => p.Period)
                .Include(i => i.Promotors)
                .ThenInclude(p => p.Person)
                .Include(i => i.Students)
                .ThenInclude(s => s.Student)
                .Include(i => i.Technologies)
                .ThenInclude(t => t.Technology)
                .Include(i => i.Validations)
                .ThenInclude(v => v.Lector)
                .Include(i => i.Validations)
                .ThenInclude(v => v.FeedBack)
                .Where(i => i.State == stateEnum)
                .ToListAsync(cancellationToken: cancellationToken);
        }
        
        public async Task<IList<Internship>> GetAllInternshipsForStudentOverview(CancellationToken cancellationToken)
        {
            return await DataContext
                .Set<Internship>()
                .Include(i => i.Address)
                .Include(i => i.Company)
                .ThenInclude(c => c.Address)
                .Include(i => i.ContactPerson)
                .Include(i => i.FeedBack)
                .Include(i => i.Courses)
                .ThenInclude(c => c.Course)
                .Include(i => i.Periods)
                .ThenInclude(p => p.Period)
                .Include(i => i.Promotors)
                .ThenInclude(p => p.Person)
                .Include(i => i.Students)
                .ThenInclude(s => s.Student)
                .Include(i => i.Technologies)
                .ThenInclude(t => t.Technology)
                .Include(i => i.Validations)
                .ThenInclude(v => v.Lector)
                .Include(i => i.Validations)
                .ThenInclude(v => v.FeedBack)
                .Where(i => i.State == InternshipStateEnum.APPROVED && i.StudentAmount != i.Students.Count)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
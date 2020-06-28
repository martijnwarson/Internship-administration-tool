using System.Linq;
using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IInternshipOverviewMapper"/>
    public class InternshipOverviewMapper
        : BaseModelMapper<Internship, InternshipOverviewDto>,
            IInternshipOverviewMapper
    {
        public InternshipOverviewMapper(
            IAddressMapper addressMapper,
            IPersonMapper<Person, PersonDto> personMapper,
            ICourseMapper courseMapper,
            IPeriodMapper periodMapper,
            ITechnologyMapper technologyMapper,
            IFeedBackMapper feedBackMapper)
        {
            AddressMapper = addressMapper;
            PersonMapper = personMapper;
            CourseMapper = courseMapper;
            PeriodMapper = periodMapper;
            TechnologyMapper = technologyMapper;
            FeedBackMapper = feedBackMapper;
        }

        public IAddressMapper AddressMapper { get; }
        public IPersonMapper<Person, PersonDto> PersonMapper { get; }
        public ICourseMapper CourseMapper { get; }
        public IPeriodMapper PeriodMapper { get; }
        public ITechnologyMapper TechnologyMapper { get; }
        public IFeedBackMapper FeedBackMapper { get; }

        public override void OnMap(Internship source, InternshipOverviewDto destination)
        {
            base.OnMap(source, destination);
            destination.Title = source.Title;
            destination.Description = source.Description;
            destination.State = source.State;
            destination.Address = AddressMapper.Map(source.Address);
            destination.Application = source.Application;
            destination.Promotors = PersonMapper.Map(source.Promotors.Select(p => p.Person)).ToArray();
            destination.Reimbursement = source.Reimbursement;
            destination.Remarks = source.Remarks;
            destination.Resumee = source.Résumée;
            destination.Technologies = TechnologyMapper.Map(source.Technologies.Select(t => t.Technology)).ToArray();
            destination.ContactPerson = PersonMapper.Map(source.ContactPerson);
            destination.Courses  = CourseMapper.Map(source.Courses.Select(c => c.Course)).ToArray();
            destination.ResearchTopic = source.ResearchTopic;
            destination.Periods = PeriodMapper.Map(source.Periods.Select(p => p.Period)).ToArray();
            destination.StudentAmount = source.StudentAmount;
            destination.Students = PersonMapper.Map(source.Students.Select(s => s.Student)).ToArray();
            destination.TechDescription = source.TechDescription;
            destination.NrOfSupportEmployees = source.NrOfSupportEmployees;
            destination.Conditions = source.Conditions;
            destination.CompanyId = source.Company.Id;
            destination.FeedBack = FeedBackMapper.Map(source.FeedBack);
        }
    }
}
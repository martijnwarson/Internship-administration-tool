using System.Linq;
using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IInternshipMapper"/>
    public class InternshipListStudentMapper
        : BaseModelMapper<Internship, InternshipListStudentsDto>,
            IInternshipListStudentMapper
    {
        public ITechnologyMapper TechnologyMapper { get; }
        public ICourseMapper CourseMapper { get; }

        public InternshipListStudentMapper(
            ITechnologyMapper technologyMapper,
            ICourseMapper courseMapper)
        {
            TechnologyMapper = technologyMapper;
            CourseMapper = courseMapper;
        }
        public override void OnMap(Internship source, InternshipListStudentsDto destination)
        {
            base.OnMap(source, destination);
            destination.Title = source.Title;
            destination.Company = source.Company.Name;
            destination.Technologies = TechnologyMapper.Map(source.Technologies.Select(t => t.Technology)).ToArray();
            destination.Courses  = CourseMapper.Map(source.Courses.Select(c => c.Course)).ToArray();
            destination.State = source.State;
        }
        
    }
}
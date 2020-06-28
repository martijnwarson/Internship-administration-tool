using System.Diagnostics.Tracing;
using System.Linq;
using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IStudentOverviewMapper"/>
    public class StudentOverviewMapper
        : PersonMapper<Student, StudentOverviewDto>,
            IStudentOverviewMapper
    {
        public StudentOverviewMapper(
            ICourseMapper courseMapper,
            IAddressMapper addressMapper,
            IInternshipListMapper internshipListMapper)
        {
            CourseMapper = courseMapper;
            AddressMapper = addressMapper;
            InternshipListMapper = internshipListMapper;
        }
        public ICourseMapper CourseMapper { get; set; }
        public IAddressMapper AddressMapper { get; set; }
        public IInternshipListMapper InternshipListMapper { get; set; }

        public override void OnMap(Student source, StudentOverviewDto destination)
        { 
            base.OnMap(source, destination);
            destination.Name = source.Name;
            destination.FirstName = source.FirstName;
            destination.Email = source.Email;
            destination.Address = AddressMapper.Map(source.Address);
            destination.Course = CourseMapper.Map(source.Course);
            destination.Favorites = InternshipListMapper.Map(source.Favorites.Select(si => si.Internship)).ToArray();
        }
    }
}
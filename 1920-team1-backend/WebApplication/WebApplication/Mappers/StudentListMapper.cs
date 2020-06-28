using System.Diagnostics.Tracing;
using System.Linq;
using WebApplication.Dtos;
using WebApplication.Managers.Interfaces;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IStudentListMapper"/>
    public class StudentListMapper
        : PersonMapper<Student, StudentListDto>,
            IStudentListMapper
    {
        
        public StudentListMapper(
            ICourseMapper courseMapper)
        {
            CourseMapper = courseMapper;
        }
        public ICourseMapper CourseMapper { get; set; }
        
        public override void OnMap(Student source, StudentListDto destination)
        { 
            base.OnMap(source, destination);
            destination.Course = CourseMapper.Map(source.Course);
        }
    }
}
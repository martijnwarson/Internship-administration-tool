using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="ICourseMapper"/>
    public class CourseMapper
        : BaseModelMapper<Course, CourseDto>
            , ICourseMapper
    {
        public override void OnMap(Course source, CourseDto destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
        }

        public override void OnMap(CourseDto source, Course destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
        }
    }
}
using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    /// <inheritdoc cref="IBidirectionalBaseMapper{TModel,TDto}"/>
    public interface ICourseMapper : IBidirectionalBaseMapper<Course, CourseDto>
    {
    }
}
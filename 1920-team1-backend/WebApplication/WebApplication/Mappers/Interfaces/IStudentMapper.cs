using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    /// <inheritdoc cref="IBidirectionalBaseMapper{TModel,TDto}"/>
    public interface IStudentMapper
        : IBidirectionalBaseMapper<Student, StudentDto>
    {
    }
}
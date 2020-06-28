using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    /// <inheritdoc cref="IBidirectionalBaseMapper{TModel,TDto}" />
    public interface IInternshipMapper
        : IBidirectionalBaseMapper<Internship, Dtos.InternshipDto>
    {
    }
}
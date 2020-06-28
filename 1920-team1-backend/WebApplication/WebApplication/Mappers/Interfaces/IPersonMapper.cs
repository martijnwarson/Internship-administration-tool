using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    /// <inheritdoc cref="IBidirectionalBaseMapper{TModel,TDto}"/>
    public interface IPersonMapper<TModel, TDto>
        : IBidirectionalBaseMapper<TModel, TDto>
    where TModel: Person
    where TDto: Dtos.PersonDto
    {
    }
}
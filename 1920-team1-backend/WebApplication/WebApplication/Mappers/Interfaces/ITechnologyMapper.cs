using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    /// <inheritdoc cref="IBidirectionalBaseMapper{TModel,TDto}"/>
    public interface ITechnologyMapper : IBidirectionalBaseMapper<Technology, TechnologyDto>
    {
    }
}
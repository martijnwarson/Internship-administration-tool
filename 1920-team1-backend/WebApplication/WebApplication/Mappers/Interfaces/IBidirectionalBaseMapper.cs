using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    /// <inheritdoc cref="IToDtoBaseModelMapper{TModel,TDto}"/>
    /// <inheritdoc cref="IToModelBaseModelMapper{TModel,TDto}"/>
    public interface IBidirectionalBaseMapper<TModel, TDto>
        : IToDtoBaseModelMapper<TModel, TDto>,
            IToModelBaseModelMapper<TModel, TDto>
        where TModel : BaseModel
        where TDto : BaseDto
    {
    }
}
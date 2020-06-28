using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    /// <inheritdoc cref="IToDtoBaseModelMapper{TModel,TDto}"/>
    public interface IValidationOverviewMapper
        : IToDtoBaseModelMapper<Validation, ValidationOverviewDto>
    {
        
    }
}
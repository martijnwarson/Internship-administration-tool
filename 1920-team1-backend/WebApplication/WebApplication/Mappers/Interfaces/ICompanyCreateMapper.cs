using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    /// <inheritdoc cref="IBidirectionalBaseMapper{TModel,TDto}" />
    public interface ICompanyCreateMapper
        : IToModelBaseModelMapper<Company, Dtos.CompanyCreateDto>
    {
    }
}
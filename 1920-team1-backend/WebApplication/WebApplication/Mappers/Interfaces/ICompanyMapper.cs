using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    /// <inheritdoc cref="IBidirectionalBaseMapper{TModel,TDto}" />
    public interface ICompanyMapper
        : IBidirectionalBaseMapper<Company, Dtos.CompanyDto>
    {
        
    }
}
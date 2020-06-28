using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    /// <inheritdoc cref="IBidirectionalBaseMapper{TModel,TDto}"/>
    public interface IAddressMapper
        : IBidirectionalBaseMapper<Address, Dtos.AddressDto>
    {
    }
}
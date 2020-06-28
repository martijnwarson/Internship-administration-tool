using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IAddressMapper"/>
    public class AddressMapper
        : BaseModelMapper<Address, Dtos.AddressDto>,
            IAddressMapper
    {
        public override void OnMap(Dtos.AddressDto source, Address destination)
        {
            base.OnMap(source, destination);
            destination.Street = source?.Street;
            destination.Number = source?.Number;
            destination.Box = source?.Box;
            destination.ZipCode = source?.ZipCode;
            destination.City = source?.City;
            destination.Country = source?.Country;
        }

        public override void OnMap(Address source, Dtos.AddressDto destination)
        {
            base.OnMap(source, destination);
            destination.Street = source?.Street;
            destination.Number = source?.Number;
            destination.Box = source?.Box;
            destination.ZipCode = source?.ZipCode;
            destination.City = source?.City;
            destination.Country = source?.Country;
        }
    }
}
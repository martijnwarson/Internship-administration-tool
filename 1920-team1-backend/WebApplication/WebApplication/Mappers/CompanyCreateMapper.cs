using WebApplication.Dtos;
using WebApplication.Enums;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="ICompanyCreateMapper"/>
    public class CompanyCreateMapper
        : BaseModelMapper<Company, CompanyCreateDto>,
            ICompanyCreateMapper
    {
        public CompanyCreateMapper(
            IPersonMapper<Person, PersonDto> personMapper,
            IAddressMapper addressMapper)
        {
            PersonMapper = personMapper;
            AddressMapper = addressMapper;
        }
        
        public IPersonMapper<Person, PersonDto> PersonMapper { get; }
        
        public IAddressMapper AddressMapper { get; }
        
        
        public override void OnMap(CompanyCreateDto source, Company destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
            destination.AmountOfEmployees = source.AmountOfEmployees;
            destination.AmountOfEmployeesIt = source.AmountOfEmployeesIt;
            destination.ContactPerson = PersonMapper.Map(source.ContactPerson);
            destination.Address = AddressMapper.Map(source.Address);
        }
    }
}
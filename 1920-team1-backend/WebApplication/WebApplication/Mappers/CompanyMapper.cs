using System.ComponentModel;
using System.Security;
using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="ICompanyMapper"/>
    public class CompanyMapper
        : BaseModelMapper<Company, CompanyDto>,
            ICompanyMapper
    {
        public CompanyMapper(
            IPersonMapper<Person, PersonDto> personMapper,
            IAddressMapper addressMapper)
        {
            PersonMapper = personMapper;
            AddressMapper = addressMapper;
        }
        
        public IPersonMapper<Person, PersonDto> PersonMapper { get; }
        
        public IAddressMapper AddressMapper { get; }
        
        public override void OnMap(CompanyDto source, Company destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
            destination.AmountOfEmployees = source.AmountOfEmployees;
            destination.AmountOfEmployeesIt = source.AmountOfEmployeesIt;
            destination.ContactPerson = PersonMapper.Map(source.ContactPerson);
            destination.Address = AddressMapper.Map(source.Address);
            destination.State = source.State;
        }

        public override void OnMap(Company source, CompanyDto destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
            destination.AmountOfEmployees = source.AmountOfEmployees;
            destination.AmountOfEmployeesIt = source.AmountOfEmployeesIt;
            destination.ContactPerson = PersonMapper.Map(source.ContactPerson);
            destination.Address = AddressMapper.Map(source.Address);
            destination.State = source.State;
        }
    }
}
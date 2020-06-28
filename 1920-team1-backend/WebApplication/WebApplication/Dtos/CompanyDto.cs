using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class CompanyDto 
        : BaseDto, 
            IEquatable<CompanyDto>
    {
        [DataMember] 
        [Required] 
        public string Name { get; set; }
        
        [DataMember] 
        [Required] 
        public int AmountOfEmployees { get; set; }
        
        [DataMember] 
        [Required] 
        public int AmountOfEmployeesIt { get; set; }
        
        [DataMember] 
        [Required] 
        public AddressDto Address { get; set; }

        [DataMember] 
        [Required] 
        public PersonDto ContactPerson { get; set; }
        
        [DataMember]
        public CompanyStateEnum State { get; set; }

        public bool Equals(CompanyDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && string.Equals(Name, other.Name, StringComparison.InvariantCulture)
                   && AmountOfEmployees == other.AmountOfEmployees
                   && AmountOfEmployeesIt == other.AmountOfEmployeesIt
                   && Equals(Address, other.Address)
                   && Equals(ContactPerson, other.ContactPerson)
                   && State == other.State;
        }

        public override bool Equals(object obj)
            => Equals(obj as CompanyDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Name, StringComparer.InvariantCulture);
            hashCode.Add(AmountOfEmployees);
            hashCode.Add(AmountOfEmployeesIt);
            hashCode.Add(Address);
            hashCode.Add(ContactPerson);
            hashCode.Add(State);
            return hashCode.ToHashCode();
        }
    }
}
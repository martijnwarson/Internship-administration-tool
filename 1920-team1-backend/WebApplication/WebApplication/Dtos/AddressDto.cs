using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class AddressDto
        : BaseDto,
            IEquatable<AddressDto>
    {
        [DataMember]
        [Required]
        public string Street { get; set; }

        [DataMember]
        [Required]
        public string Number { get; set; }
        
        [DataMember]
        public string Box { get; set; }

        [DataMember]
        [Required]
        public string ZipCode { get; set; }

        [DataMember]
        [Required]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        public bool Equals(AddressDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && string.Equals(Street, other.Street, StringComparison.InvariantCulture)
                   && string.Equals(Number, other.Number, StringComparison.InvariantCulture)
                   && string.Equals(Box, other.Box, StringComparison.InvariantCulture)
                   && string.Equals(ZipCode, other.ZipCode, StringComparison.InvariantCulture)
                   && string.Equals(City, other.City, StringComparison.InvariantCulture)
                   && string.Equals(Country, other.Country, StringComparison.InvariantCulture);
        }

        public override bool Equals(object obj)
            => Equals(obj as AddressDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Street, StringComparer.InvariantCulture);
            hashCode.Add(Number, StringComparer.InvariantCulture);
            hashCode.Add(Box, StringComparer.InvariantCulture);
            hashCode.Add(ZipCode, StringComparer.InvariantCulture);
            hashCode.Add(City, StringComparer.InvariantCulture);
            hashCode.Add(Country, StringComparer.InvariantCulture);
            return hashCode.ToHashCode();
        }
    }
}
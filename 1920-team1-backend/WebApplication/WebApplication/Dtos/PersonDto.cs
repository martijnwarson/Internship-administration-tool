using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class PersonDto
        : BaseDto,
            IEquatable<PersonDto>
    {
        [DataMember]
        [Required]
        public string Name { get; set; }
        
        [DataMember]
        [Required]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        public string Title { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }
        
        [DataMember]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Equals(PersonDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && string.Equals(Name, other.Name, StringComparison.InvariantCulture)
                   && string.Equals(FirstName, other.FirstName, StringComparison.InvariantCulture)
                   && string.Equals(Title, other.Title, StringComparison.InvariantCulture)
                   && string.Equals(TelephoneNumber, other.TelephoneNumber, StringComparison.InvariantCulture)
                   && string.Equals(Email, other.Email, StringComparison.InvariantCulture);
        }

        public override bool Equals(object obj)
            => Equals(obj as PersonDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Name, StringComparer.InvariantCulture);
            hashCode.Add(FirstName, StringComparer.InvariantCulture);
            hashCode.Add(Title, StringComparer.InvariantCulture);
            hashCode.Add(TelephoneNumber, StringComparer.InvariantCulture);
            hashCode.Add(Email, StringComparer.InvariantCulture);
            return hashCode.ToHashCode();
        }
    }
}
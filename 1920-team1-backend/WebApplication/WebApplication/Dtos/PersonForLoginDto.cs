using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using WebApplication.Models;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class PersonForLoginDto
        : IEquatable<PersonForLoginDto>
    {
        [DataMember] [Required] public string Username { get; set; }
        [DataMember] [Required] public string Password { get; set; }

        public bool Equals(PersonForLoginDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Username, other.Username, StringComparison.InvariantCulture)
                   && string.Equals(Password, other.Password, StringComparison.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PersonForLoginDto) obj);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Username, StringComparer.InvariantCulture);
            hashCode.Add(Password, StringComparer.InvariantCulture);
            return hashCode.ToHashCode();
        }
    }
}

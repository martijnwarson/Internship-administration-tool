using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class StudentDto
        : PersonDto,
            IEquatable<StudentDto>
    {
        [DataMember] 
        [Required]
        public AddressDto Address { get; set; }
        
        [DataMember] 
        [Required]
        public long CourseId { get; set; }

        [DataMember] 
        public long[] FavoritesIds { get; set; }

        public bool Equals(StudentDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && Equals(Address, other. Address)
                   && CourseId.Equals(other.CourseId)
                   && FavoritesIds.SequenceEqual(other.FavoritesIds);
        }

        public override bool Equals(object obj)
            => Equals(obj as StudentDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Address);
            hashCode.Add(CourseId);
            hashCode.Add(FavoritesIds);
            return hashCode.ToHashCode();
        }
    }
}
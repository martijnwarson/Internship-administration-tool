using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class StudentOverviewDto
        : PersonDto,
            IEquatable<StudentOverviewDto>
    {
        [DataMember] 
        [Required]
        public AddressDto Address { get; set; }

        [DataMember] 
        [Required] 
        public CourseDto Course { get; set; }

        [DataMember]
        public InternshipListDto[] Favorites { get; set; }

        public bool Equals(StudentOverviewDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && Equals(Address, other.Address)
                   && Equals(Course, other.Course) 
                   && Equals(Favorites, other.Favorites);
        }

        public override bool Equals(object obj)
            => Equals(obj as StudentOverviewDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Address);
            hashCode.Add(Course);
            hashCode.Add(Favorites);
            return hashCode.ToHashCode();
        }
    }
}
    
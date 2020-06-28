using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class StudentListDto
        : PersonDto,
            IEquatable<StudentListDto>
    {
        [DataMember] 
        [Required] 
        public CourseDto Course { get; set; }

        public bool Equals(StudentListDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && Equals(Course, other.Course);
        }

        public override bool Equals(object obj)
            => Equals(obj as StudentListDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Course);
            return hashCode.ToHashCode();
        }
    }
}
    
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class CourseDto
        : BaseDto,
            IEquatable<CourseDto>
    {
        [DataMember]
        [Required]
        [NotNull]
        public string Name { get; set; }

        public bool Equals(CourseDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && string.Equals(Name, other.Name, StringComparison.InvariantCulture);
        }

        public override bool Equals(object obj)
            => Equals(obj as CourseDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Name, StringComparer.InvariantCulture);
            return hashCode.ToHashCode();
        }
    }
}
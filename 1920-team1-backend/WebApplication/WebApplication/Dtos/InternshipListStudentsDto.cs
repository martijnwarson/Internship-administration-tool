using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class InternshipListStudentsDto
        : BaseDto,
            IEquatable<InternshipListStudentsDto>
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Company { get; set; }
        
        [DataMember]
        public TechnologyDto[] Technologies { get; set; }
        
        [DataMember]
        public CourseDto[] Courses { get; set; }

        [DataMember]
        public InternshipStateEnum? State { get; set; }

        public bool Equals(InternshipListStudentsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && string.Equals(Title, other.Title, StringComparison.InvariantCulture)
                   && string.Equals(Company, other.Company, StringComparison.InvariantCulture)
                   && Equals(Technologies, other.Technologies)
                   && Equals(Courses, other.Courses)
                   && State == other.State;
        }

        public override bool Equals(object obj)
            => Equals(obj as InternshipListStudentsDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Title, StringComparer.InvariantCulture);
            hashCode.Add(Company, StringComparer.InvariantCulture);
            hashCode.Add(Technologies);
            hashCode.Add(Courses);
            hashCode.Add(State);
            return hashCode.ToHashCode();
        }
    }
}
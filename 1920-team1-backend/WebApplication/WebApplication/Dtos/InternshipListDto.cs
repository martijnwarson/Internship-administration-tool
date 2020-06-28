using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class InternshipListDto
        : BaseDto,
            IEquatable<InternshipListDto>
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Company { get; set; }

        [DataMember]
        public InternshipStateEnum? State { get; set; }

        public bool Equals(InternshipListDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && string.Equals(Title, other.Title, StringComparison.InvariantCulture)
                   && string.Equals(Company, other.Company, StringComparison.InvariantCulture)
                   && State == other.State;
        }

        public override bool Equals(object obj)
            => Equals(obj as InternshipListDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Title, StringComparer.InvariantCulture);
            hashCode.Add(Company, StringComparer.InvariantCulture);
            hashCode.Add(State);
            return hashCode.ToHashCode();
        }
    }
}
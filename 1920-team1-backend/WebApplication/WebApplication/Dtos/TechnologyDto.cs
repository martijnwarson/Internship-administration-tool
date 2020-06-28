using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [Serializable]
    [DataContract]
    public class TechnologyDto
        : BaseDto, IEquatable<TechnologyDto>
    {
        [DataMember]
        [Required]
        [NotNull]
        public string Name { get; set; }

        public bool Equals(TechnologyDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && string.Equals(Name, other.Name, StringComparison.InvariantCulture);
        }

        public override bool Equals(object obj)
            => Equals(obj as TechnologyDto);

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
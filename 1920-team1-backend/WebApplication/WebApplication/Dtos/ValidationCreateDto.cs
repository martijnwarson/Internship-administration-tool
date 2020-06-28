using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class ValidationCreateDto : IEquatable<ValidationCreateDto>
    {
        [DataMember]
        [Required]
        public long[] LectorIds { get; set; }

        public bool Equals(ValidationCreateDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return LectorIds.SequenceEqual(other.LectorIds);
        }

        public override bool Equals(object obj)
            => Equals(obj as ValidationCreateDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return (LectorIds != null ? LectorIds.GetHashCode() : 0);
        }
    }
}
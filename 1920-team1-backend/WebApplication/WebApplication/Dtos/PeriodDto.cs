using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [Serializable]
    [DataContract]
    public class PeriodDto
        : BaseDto,
            IEquatable<PeriodDto>
    {
        [DataMember]
        [Required]
        [NotNull]
        public string Name { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }

        public bool Equals(PeriodDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && string.Equals(Name, other.Name, StringComparison.InvariantCulture)
                   && From.Equals(other.From) && To.Equals(other.To);
        }

        public override bool Equals(object obj)
            => Equals(obj as PeriodDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Name, StringComparer.InvariantCulture);
            hashCode.Add(From);
            hashCode.Add(To);
            return hashCode.ToHashCode();
        }
    }
}
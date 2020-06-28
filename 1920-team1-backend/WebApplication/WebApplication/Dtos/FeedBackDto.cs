using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [Serializable]
    [DataContract]
    public class FeedBackDto
        : BaseDto,
            IEquatable<FeedBackDto>
    {
        [DataMember]
        public string Value { get; set; }
        
        [DataMember]
        public DateTime ModifiedAt { get; set; }
        
        [DataMember]
        public DateTime CreatedAt { get; set; }

        public bool Equals(FeedBackDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && string.Equals(Value, other.Value, StringComparison.InvariantCulture)
                   && ModifiedAt.Equals(other.ModifiedAt)
                   && CreatedAt.Equals(other.CreatedAt);
        }

        public override bool Equals(object obj)
            => Equals(obj as FeedBackDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Value, StringComparer.InvariantCulture);
            hashCode.Add(ModifiedAt);
            hashCode.Add(CreatedAt);
            return hashCode.ToHashCode();
        }
    }
}
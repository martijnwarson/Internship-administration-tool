using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using WebApplication.Enums;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class ValidationUpdateDto
        : BaseDto,
            IEquatable<ValidationUpdateDto>
    {
        [DataMember]
        public FeedBackDto Feedback { get; set; }

        [DataMember]
        public ValidationStateEnum State{ get; set; }

        public bool Equals(ValidationUpdateDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && Equals(Feedback, other.Feedback)
                   && State == other.State;
        }

        public override bool Equals(object obj)
            => Equals(obj as ValidationUpdateDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Feedback.GetHashCode(), State.GetHashCode());
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using WebApplication.Enums;

namespace WebApplication.Dtos
{
    [Serializable]
    [DataContract]
    public class InternShipUpdateStateDto
        : IEquatable<InternShipUpdateStateDto>
    {
        [DataMember]
        [Required]
        public InternshipStateEnum State { get; set; }

        [DataMember]
        public FeedBackDto FeedBack { get; set; }

        public bool Equals(InternShipUpdateStateDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && Equals(FeedBack, other.FeedBack)
                   && State == other.State;
        }

        public override bool Equals(object obj)
            => Equals(obj as InternShipUpdateStateDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), FeedBack.GetHashCode(), State.GetHashCode());
        }
    }
}
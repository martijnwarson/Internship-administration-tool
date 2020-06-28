using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class ValidationOverviewDto
        : BaseDto,
            IEquatable<ValidationOverviewDto>
    {
        [DataMember]
        public PersonDto Lector { get; set; }
        
        [DataMember]
        public FeedBackDto FeedBack { get; set; }

        [DataMember]
        public ValidationStateEnum State { get; set; }
        
        [DataMember]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public bool Equals(ValidationOverviewDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && Equals(Lector, other.Lector)
                   && Equals(FeedBack, other.FeedBack)
                   && State == other.State
                   && Date.Equals(other.Date);
        }

        public override bool Equals(object obj)
            => Equals(obj as ValidationOverviewDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Lector, FeedBack, (int) State, Date);
        }
    }
}
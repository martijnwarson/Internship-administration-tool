using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using WebApplication.Enums;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class UpdateStateCompanyDto
        : BaseDto,
            IEquatable<UpdateStateCompanyDto>
    {
        [DataMember]
        public FeedBackDto FeedBack { get; set; }

        [DataMember]
        public CompanyStateEnum CompanyState { get; set; }

        public bool Equals(UpdateStateCompanyDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && Equals(FeedBack, other.FeedBack) 
                   && CompanyState == other.CompanyState;
        }

        public override bool Equals(object obj)
            => Equals(obj as UpdateStateCompanyDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), FeedBack.GetHashCode(), CompanyState.GetHashCode());
        }
    }
}
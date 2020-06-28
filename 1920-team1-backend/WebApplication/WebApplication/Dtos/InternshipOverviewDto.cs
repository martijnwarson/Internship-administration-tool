using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class InternshipOverviewDto
    : BaseDto,
        IEquatable<InternshipOverviewDto>
    {
        [DataMember]
        public TechnologyDto[] Technologies { get; set; }
        
        [DataMember]
        public CourseDto[] Courses { get; set; }
        
        [DataMember]
        public PeriodDto[] Periods { get; set; }
        
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public long CompanyId { get; set; }

        [DataMember]
        public PersonDto ContactPerson { get; set; }

        [DataMember]
        public PersonDto[] Promotors { get; set; }
        
        [DataMember]
        public AddressDto Address { get; set; }

        [DataMember]
        public string TechDescription { get; set; }

        [DataMember]
        public string ResearchTopic { get; set; }

        [DataMember]
        public bool? Application { get; set; }

        [DataMember]
        public bool? Resumee { get; set; }

        [DataMember]
        public bool? Reimbursement { get; set; }

        [DataMember]
        public int StudentAmount { get; set; }
        
        [DataMember]
        public PersonDto[] Students { get; set; }

        [DataMember]
        public int NrOfSupportEmployees { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string Conditions { get; set; }

        [DataMember]
        public InternshipStateEnum? State { get; set; }

        [DataMember]
        public FeedBackDto FeedBack { get; set; }

        public bool Equals(InternshipOverviewDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && Equals(Technologies, other.Technologies)
                   && Equals(Courses, other.Courses)
                   && Equals(Periods, other.Periods)
                   && string.Equals(Title, other.Title, StringComparison.InvariantCulture)
                   && string.Equals(Description, other.Description, StringComparison.InvariantCulture)
                   && Equals(CompanyId, other.CompanyId)
                   && Equals(ContactPerson, other.ContactPerson)
                   && Equals(Promotors, other.Promotors)
                   && Equals(Address, other.Address)
                   && string.Equals(TechDescription, other.TechDescription, StringComparison.InvariantCulture)
                   && string.Equals(ResearchTopic, other.ResearchTopic, StringComparison.InvariantCulture)
                   && Application == other.Application
                   && Resumee == other.Resumee
                   && Reimbursement == other.Reimbursement
                   && StudentAmount == other.StudentAmount
                   && Equals(Students, other.Students)
                   && string.Equals(Remarks, other.Remarks, StringComparison.InvariantCulture)
                   && string.Equals(Conditions, other.Conditions, StringComparison.InvariantCulture)
                   && State == other.State
                   && Equals(FeedBack, other.FeedBack); ;
        }

        public override bool Equals(object obj)
            => Equals(obj as InternshipOverviewDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Technologies);
            hashCode.Add(Courses);
            hashCode.Add(Periods);
            hashCode.Add(Title, StringComparer.InvariantCulture);
            hashCode.Add(Description, StringComparer.InvariantCulture);
            hashCode.Add(ContactPerson);
            hashCode.Add(CompanyId);
            hashCode.Add(Promotors);
            hashCode.Add(Address);
            hashCode.Add(TechDescription, StringComparer.InvariantCulture);
            hashCode.Add(ResearchTopic, StringComparer.InvariantCulture);
            hashCode.Add(Application);
            hashCode.Add(Resumee);
            hashCode.Add(Reimbursement);
            hashCode.Add(StudentAmount);
            hashCode.Add(Students);
            hashCode.Add(Remarks, StringComparer.InvariantCulture);
            hashCode.Add(Conditions, StringComparer.InvariantCulture);
            hashCode.Add(State);
            hashCode.Add(FeedBack);
            return hashCode.ToHashCode();
        }
    }
}
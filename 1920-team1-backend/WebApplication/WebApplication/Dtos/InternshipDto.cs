using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using WebApplication.Enums;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class InternshipDto
        : BaseDto,
            IEquatable<InternshipDto>
    {
        [DataMember]
        [Required]
        public long[] TechnologyIds { get; set; }
        
        [DataMember]
        [Required]
        public long[] CourseIds { get; set; }
        
        [DataMember]
        [Required]
        public long[] PeriodIds { get; set; }
        
        [DataMember]
        [Required]
        public string Title { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public long CompanyId { get; set; }

        [DataMember]
        [Required]
        public PersonDto ContactPerson { get; set; }

        [DataMember]
        [Required]
        public PersonDto[] Promotors { get; set; }
        
        [DataMember]
        public AddressDto Address { get; set; }

        [DataMember]
        [Required]
        public string TechDescription { get; set; }

        [DataMember]
        [Required]
        public string ResearchTopic { get; set; }

        [DataMember]
        [Required]
        public bool? Application { get; set; }

        [DataMember]
        [Required]
        public bool? Resumee { get; set; }

        [DataMember]
        [Required]
        public bool? Reimbursement { get; set; }

        [DataMember]
        [Range(0, 2)]
        public int StudentAmount { get; set; }
        
        [DataMember]
        public long[] StudentIds { get; set; }

        [DataMember]
        [Required]
        public int NrOfSupportEmployees { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string Conditions { get; set; }

        [DataMember]
        public InternshipStateEnum? State { get; set; }

        [DataMember]
        public FeedBackDto FeedBack { get; set; }

        public bool Equals(InternshipDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && TechnologyIds.SequenceEqual(other.TechnologyIds)
                   && CourseIds.SequenceEqual(other.CourseIds)
                   && PeriodIds.SequenceEqual(other.PeriodIds)
                   && string.Equals(Title, other.Title, StringComparison.InvariantCulture)
                   && string.Equals(Description, other.Description, StringComparison.InvariantCulture)
                   && Equals(CompanyId, other.CompanyId)
                   && Equals(ContactPerson, other.ContactPerson)
                   && Promotors.SequenceEqual(other.Promotors)
                   && Equals(Address, other.Address)
                   && string.Equals(TechDescription, other.TechDescription, StringComparison.InvariantCulture)
                   && string.Equals(ResearchTopic, other.ResearchTopic, StringComparison.InvariantCulture)
                   && Application == other.Application
                   && Resumee == other.Resumee
                   && Reimbursement == other.Reimbursement
                   && StudentAmount == other.StudentAmount
                   && NrOfSupportEmployees == other.NrOfSupportEmployees
                   && string.Equals(Remarks, other.Remarks, StringComparison.InvariantCulture)
                   && string.Equals(Conditions, other.Conditions, StringComparison.InvariantCulture)
                   && StudentIds.SequenceEqual(other.StudentIds)
                   && State == other.State
                   && Equals(FeedBack, other.FeedBack);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as InternshipDto);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(TechnologyIds);
            hashCode.Add(CourseIds);
            hashCode.Add(PeriodIds);
            hashCode.Add(Title, StringComparer.InvariantCulture);
            hashCode.Add(Description, StringComparer.InvariantCulture);
            hashCode.Add(CompanyId);
            hashCode.Add(ContactPerson);
            hashCode.Add(Promotors);
            hashCode.Add(Address);
            hashCode.Add(TechDescription, StringComparer.InvariantCulture);
            hashCode.Add(ResearchTopic, StringComparer.InvariantCulture);
            hashCode.Add(Application);
            hashCode.Add(Resumee);
            hashCode.Add(Reimbursement);
            hashCode.Add(StudentAmount);
            hashCode.Add(NrOfSupportEmployees);
            hashCode.Add(Remarks, StringComparer.InvariantCulture);
            hashCode.Add(Conditions, StringComparer.InvariantCulture);
            hashCode.Add(StudentIds);
            hashCode.Add(State);
            hashCode.Add(FeedBack);
            return hashCode.ToHashCode();
        }
    }
}
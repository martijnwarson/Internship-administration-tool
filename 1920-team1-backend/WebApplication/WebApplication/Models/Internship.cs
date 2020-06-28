using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using WebApplication.Enums;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class Internship : BaseModel
    {
        public Internship()
        {
            Courses = new List<InternshipCourse>();
            Technologies = new List<InternshipTechnology>();
            Validations = new List<Validation>();
            Students = new List<InternshipStudent>();
            Promotors = new List<InternshipPerson>();
            Periods = new List<InternshipPeriod>();
        }
        [DataMember]
        [Required]
        [NotNull]
        public Company Company { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public string Title { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public string Description { get; set; }

        [DataMember]
        [Required]
        [NotNull]
        public ICollection<InternshipCourse> Courses { get; }

        [DataMember]
        [Required]
        [NotNull]
        public ICollection<InternshipTechnology> Technologies { get; }

        [DataMember]
        [Required]
        [NotNull]
        public Person ContactPerson { get; set; }
                
        [DataMember]
        [Required]
        [NotNull]
        public ICollection<InternshipPerson> Promotors { get; }

        [DataMember]
        [Required]
        public Address Address { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public string TechDescription { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public string ResearchTopic { get; set; }
        
        [DataMember]
        [Required]
        public bool? Application { get; set; }
        
        [DataMember]
        [Required]
        public bool? Résumée { get; set; }
        
        [DataMember]
        [Required]
        public bool? Reimbursement { get; set; }
        
        [DataMember]
        [Range(0,2)]
        public int StudentAmount { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public ICollection<InternshipStudent> Students { get; }

        [DataMember]
        [Required]
        [NotNull]
        public int NrOfSupportEmployees { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string Conditions { get; set; }

        [DataMember]
        [Required]
        [NotNull]
        public ICollection<Validation> Validations { get; }
        
        [DataMember]
        [Required]
        [NotNull]
        public ICollection<InternshipPeriod> Periods { get; }

        [DataMember]
        [Required]
        [NotNull]
        public InternshipStateEnum State { get; set; }
        
        [DataMember]
        public FeedBack FeedBack { get; set; }
    }
}
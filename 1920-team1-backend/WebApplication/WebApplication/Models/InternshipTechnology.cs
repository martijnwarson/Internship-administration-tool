using System;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class InternshipTechnology
    {
        public InternshipTechnology()
        {
        }
        public InternshipTechnology(Internship internship, Technology technology)
        {
            Internship = internship;
            Technology = technology;
        }
        
        [DataMember]
        public long InternshipId { get; set; }
        
        [DataMember]
        public Internship Internship { get; set; }
        
        [DataMember]
        public long TechnologyId { get; set; }
        
        [DataMember]
        public Technology Technology { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using WebApplication.Enums;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class Company : BaseModel
    {
        public Company()
        {
            Internships = new List<Internship>();    
        }
        
        [DataMember]
        [Required]
        [NotNull]
        public string Name { get; set; }

        [DataMember]
        [Required]
        [NotNull]
        public int AmountOfEmployees { get; set; }

        [DataMember]
        [Required]
        [NotNull]
        public int AmountOfEmployeesIt { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public Address Address { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public CompanyStateEnum State { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public Person ContactPerson { get; set; }

        [DataMember]
        [NotNull]
        public ICollection<Internship> Internships { get; }
        
        [DataMember]
        public string ReasonOfInactive { get; set; }
        
        [DataMember]
        public FeedBack FeedBack { get; set; }
    }
}
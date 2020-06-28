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
    public class Student : Person
    {
        public Student()
        {
            Favorites = new List<StudentInternShip>();
        }
        
        [DataMember]
        [Required]
        [NotNull]
        public Course Course { get; set; }

        [DataMember]
        
        [NotNull]
        public Address Address { get; set; }

        [DataMember]
        public ICollection<StudentInternShip> Favorites { get; }
    }
}
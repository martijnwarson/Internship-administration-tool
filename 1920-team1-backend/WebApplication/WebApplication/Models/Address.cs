using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class Address : BaseModel
    {
        [DataMember]
        [Required]
        [NotNull]
        public string Street { get; set; }

        [DataMember]
        [Required]
        [NotNull]
        public string Number { get; set; }
        
        [DataMember]
        public string Box { get; set; }

        [DataMember]
        [Required]
        [NotNull]
        public string ZipCode { get; set; }

        [DataMember]
        [Required]
        [NotNull]
        public string City { get; set; }

        [DataMember]
        [Required]
        [NotNull]
        public string Country { get; set; }
    }
}
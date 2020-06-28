using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using WebApplication.Enums;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class Person : BaseModel
    {
        [DataMember]
        [Required]
        [NotNull]
        public string Name { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        [NotNull]
        public string Title { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        [DataType(DataType.EmailAddress)]
        // Will be used as username for login
        public string Email { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public RoleEnum Role { get; set; }

        [DataMember]
        // For login without MS.Extentions.Identity.Core
        public byte[] PasswordHash { get; set; }

        [DataMember]
        // For login without MS.Extentions.Identity.Core
        public byte[] PasswordSalt { get; set; }
    }
}
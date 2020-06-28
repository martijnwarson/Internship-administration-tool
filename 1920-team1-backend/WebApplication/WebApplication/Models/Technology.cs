using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class Technology : BaseModel
    {
        [DataMember]
        [Required]
        [NotNull]
        public string Name { get; set; }
    }
}
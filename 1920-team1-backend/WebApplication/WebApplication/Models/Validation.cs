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
    public class Validation : BaseModel
    {
        public Validation()
        {
        }
        
        [DataMember]
        [Required]
        [NotNull]
        public Internship Internship { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public Lector Lector { get; set; }

        [DataMember]
        public FeedBack FeedBack { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        public ValidationStateEnum State { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}
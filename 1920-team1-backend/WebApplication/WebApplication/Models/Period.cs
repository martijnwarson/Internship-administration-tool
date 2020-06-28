using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class Period : BaseModel
    {
        [DataMember]
        [Required]
        [NotNull]
        public string Name { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        
        [DataMember]
        [Required]
        [NotNull]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }

        public Period(string name, DateTime from, DateTime to)
        {
            Name = name;
            From = from;
            To = to;
        }

        public Period()
        {
        }
    }
}
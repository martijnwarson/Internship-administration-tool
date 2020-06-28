using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class FeedBack : BaseModel
    {
        public FeedBack()
        {
        }

        public FeedBack(string value, DateTime? modifiedAt)
        {
            Value = value;
            CreatedAt = DateTime.Now;
            ModifiedAt = modifiedAt;
            ModifiedAt = DateTime.Now;
        }

        [DataMember]
        [Required]
        [NotNull]
        public string Value { get; set; }
        
        [DataMember]
        public DateTime? ModifiedAt { get; set; }
        
        [DataMember]
        [Required]
        public DateTime? CreatedAt { get; set; }
    }
}
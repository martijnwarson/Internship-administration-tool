using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace WebApplication.Enums
{
    [DataContract]
    [Serializable]
    public enum InternshipStateEnum
    {
        [EnumMember]
        NEW = 0,
        
        [EnumMember]
        PENDING,
        
        [EnumMember]
        [Description("TO BE MODIFIED")]
        TO_BE_MODIFIED,
        
        [EnumMember]
        APPROVED,
        
        [EnumMember]
        MODIFIED,
        
        [EnumMember]
        REJECTED,
    }
}
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace WebApplication.Enums
{
    [DataContract]
    [Serializable]
    public enum ValidationStateEnum
    {
        [EnumMember]
        NEW = 0,
        
        [EnumMember]
        APPROVED,
        
        [EnumMember]
        REJECTED,

        [EnumMember]
        [Description("QUESTIONS OR REMARKS")]
        QUESTIONS_OR_REMARKS,
    }
}
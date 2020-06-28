using System;
using System.Runtime.Serialization;

namespace WebApplication.Enums
{
    [DataContract]
    [Serializable]
    public enum CompanyStateEnum
    {
        [EnumMember]
        NEW = 0,
        
        [EnumMember]
        ACTIVE,
        
        [EnumMember]
        INACTIVE,
    }
}
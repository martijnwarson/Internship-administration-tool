using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplication.Enums
{
    [DataContract]
    [Serializable]
    public enum RoleEnum
    {
        [EnumMember]
        CONTACT = 0,
        
        [EnumMember]
        STUDENT,
        
        [EnumMember]
        LECTOR,
        
        [EnumMember]
        COÖRDINATOR,
    }
}
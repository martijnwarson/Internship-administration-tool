using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class BaseDto 
        : IEquatable<BaseDto>
    {
        [DataMember]
        public long? Id { get; set; }

        public bool Equals(BaseDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj) 
            => Equals(obj as BaseDto);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
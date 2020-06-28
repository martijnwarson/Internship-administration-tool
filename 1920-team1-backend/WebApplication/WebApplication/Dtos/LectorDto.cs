using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Dtos
{
    [DataContract]
    [Serializable]
    public class LectorDto
        : PersonDto,
            IEquatable<LectorDto>
    {
        [DataMember]
        [Required]
        public long[] CoursesIds { get; set; }


        public bool Equals(LectorDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                && CoursesIds.Equals(other.CoursesIds);
        }

        public override bool Equals(object obj)
            => Equals(obj as LectorDto);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(CoursesIds);
            return hashCode.ToHashCode();
        }
    }
}

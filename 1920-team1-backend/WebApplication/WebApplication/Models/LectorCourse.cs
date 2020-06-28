using System;
using System.Runtime.Serialization;
using Microsoft.VisualBasic.CompilerServices;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class LectorCourse
    {
        public LectorCourse()
        {
        }

        public LectorCourse(Lector lector, Course course)
        {
            Lector = lector;
            Course = course;
        }
        
        [DataMember]
        public long LectorId { get; set; }
        
        [DataMember]
        public Lector Lector { get; set; }
        
        [DataMember]
        public long CourseId { get; set; }
        
        [DataMember]
        public Course Course { get; set; }
    }
}
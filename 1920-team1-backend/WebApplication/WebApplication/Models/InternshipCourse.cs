using System;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class InternshipCourse
    {
        public InternshipCourse()
        {
        }
        public InternshipCourse(Internship internship, Course course)
        {
            Internship = internship;
            Course = course;
        }

        [DataMember]
        public long InternshipId { get; set; }
        
        [DataMember]
        public Internship Internship { get; set; }
        
        [DataMember]
        public long CourseId { get; set; }
        
        [DataMember]
        public Course Course { get; set; }
    }
}
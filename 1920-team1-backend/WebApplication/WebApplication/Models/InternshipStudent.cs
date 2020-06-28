using System;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class InternshipStudent
    {
        public InternshipStudent()
        {
        }
        public InternshipStudent(Internship internship, Student student)
        {
            Internship = internship;
            Student = student;
        }
        
        [DataMember]
        public long InternshipId { get; set; }
        
        [DataMember]
        public Internship Internship { get; set; }
        
        [DataMember]
        public long StudentId { get; set; }
        
        [DataMember]
        public Student Student { get; set; }
    }
}
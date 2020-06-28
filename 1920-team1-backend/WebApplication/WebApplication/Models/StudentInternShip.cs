using System.Runtime.Serialization;

namespace WebApplication.Models
{
    public class StudentInternShip
    {
        public StudentInternShip()
        {
        }
        
        public StudentInternShip(Student student, Internship internship)
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
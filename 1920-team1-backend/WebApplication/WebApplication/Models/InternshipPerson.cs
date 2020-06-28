using System;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class InternshipPerson
    {
        public InternshipPerson()
        {
        }
        public InternshipPerson(Internship internship, Person person)
        {
            Internship = internship;
            Person = person;
        }
        [DataMember]
        public long InternshipId { get; set; }
        
        [DataMember]
        public Internship Internship { get; set; }
        
        [DataMember]
        public long PersonId { get; set; }
        
        [DataMember]
        public Person Person { get; set; }
    }
}
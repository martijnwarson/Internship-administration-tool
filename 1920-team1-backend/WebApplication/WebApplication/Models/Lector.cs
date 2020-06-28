using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class Lector : Person
    {
        public Lector()
        {
            Courses = new List<LectorCourse>();
        }
        [DataMember]
        public ICollection<LectorCourse> Courses { get; set; }

        public void AddCourses(IEnumerable<Course> courses)
        {
            foreach (Course course in courses)
            {
                Courses. Add(new LectorCourse(this, course));
            }
        }
    }
}
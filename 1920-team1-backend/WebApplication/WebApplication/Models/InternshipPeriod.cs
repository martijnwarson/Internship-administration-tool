using System;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    [Serializable]
    public class InternshipPeriod
    {
        public InternshipPeriod()
        {
        }
        public InternshipPeriod(Internship internship, Period period)
        {
            Internship = internship;
            Period = period;
        }
        
        [DataMember]
        public long InternshipId { get; set; }
        
        [DataMember]
        public Internship Internship { get; set; }
        
        [DataMember]
        public long PeriodId { get; set; }
        
        [DataMember]
        public Period Period { get; set; }
    }
}
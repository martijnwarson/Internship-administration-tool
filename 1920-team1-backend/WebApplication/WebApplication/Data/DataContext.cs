using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Period> Periods { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lector> Lectors { get; set; }
        public DbSet<Coördinator> Coördinators { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<Validation> Validations { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Property(c => c.State).HasConversion<string>();
            modelBuilder.Entity<Internship>().Property(i => i.State).HasConversion<string>();
            modelBuilder.Entity<Person>().Property(p => p.Role).HasConversion<string>();
            modelBuilder.Entity<Validation>().Property(v => v.State).HasConversion<string>();
            
            // Unique constraint
            modelBuilder.Entity<Person>().HasIndex(p => p.Email).IsUnique();

            // ManyToMany relations
            modelBuilder.Entity<LectorCourse>().HasKey(x => new {x.LectorId, x.CourseId});
            modelBuilder.Entity<InternshipCourse>().HasKey(x => new {x.InternshipId, x.CourseId});
            modelBuilder.Entity<InternshipTechnology>().HasKey(x => new {x.InternshipId, x.TechnologyId});
            modelBuilder.Entity<InternshipStudent>().HasKey(x => new {x.InternshipId, x.StudentId});
            modelBuilder.Entity<InternshipPerson>().HasKey(x => new {x.InternshipId, x.PersonId});
            modelBuilder.Entity<InternshipPeriod>().HasKey(x => new {x.InternshipId, x.PeriodId});
            modelBuilder.Entity<StudentInternShip>().HasKey(x => new {x.StudentId, x.InternshipId});
        }
    }
}
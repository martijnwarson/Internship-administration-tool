using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication.Enums;
using WebApplication.Helpers;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="IStudentManager" />
    public class StudentManager
        : PersonManager<Student>,
            IStudentManager
    {
        // Csv Columnames
        private static int COURSE = 1;
        private static int NAME = 3;
        private static int FIRSTNAME = 4;
        private static int STREET = 5;
        private static int NUMBER = 6;
        private static int BOX = 7;
        private static int ZIPCODE = 8;
        private static int CITY = 8;
        private static int EMAIL = 11;
        private static int TELEPHONENUMBER = 12;
        
        public StudentManager(
            IStudentRepository repository,
            IPasswordGenerator passwordGenerator,
            ICourseManager courseManager,
            IEmailManager emailManager)
            : base(repository, passwordGenerator, emailManager)
        {
            StudentRepository = repository;
            CourseManager = courseManager;
        }

        private IStudentRepository StudentRepository { get; }
        public ICourseManager CourseManager { get; }

        public async Task<(string username, string password)> CreateStudentAsync(Student student,
            CancellationToken cancellationToken)
        {
            student.Role = RoleEnum.STUDENT;
            string password =
                await CreateLoginAsync(
                    student, 
                    cancellationToken);

            return (student.Email, password);
        }

        public async Task<Student> ToggleFavorite(long studentId, Internship internship, CancellationToken cancellationToken)
        {
            Student student = await StudentRepository.GetByIdAsync(studentId, cancellationToken);
            StudentInternShip studentInternShip = student.Favorites.SingleOrDefault(si => si.Internship == internship);
            if (studentInternShip == null)
            {
                student.Favorites.Add(new StudentInternShip(student, internship));
            }
            else
            {
                student.Favorites.Remove(studentInternShip);
            }

            await UpdateAsync(student, cancellationToken);
            return student;
        }

        public async Task CreateStudentsFromCsv(IFormFile file, CancellationToken cancellationToken)
        {
            ISet<Course> foundCourses = await CourseManager.GetAllAsync(cancellationToken);
            IList<Student> students = new List<Student>();
            
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                String headerline = await reader.ReadLineAsync();
                while (reader.Peek() >= 0)
                {
                    String studentLine = await reader.ReadLineAsync();
                    string[] values = studentLine?.Split(';');
                    Contract.Assume(values != null);
                    Student student = new Student
                    {
                        Title = " ",
                        Name = values[NAME],
                        FirstName = values[FIRSTNAME],
                        Email =  values[EMAIL],
                        TelephoneNumber = values[TELEPHONENUMBER],
                        Address = new Address
                        {
                            Street = values[STREET],
                            Number = values[NUMBER],
                            Box = values[BOX],
                            ZipCode = values[ZIPCODE],
                            City = values[CITY],
                            Country = "België"
                        },
                        Role = RoleEnum.STUDENT
                    };
                    student.Course = foundCourses.SingleOrDefault(c => c.Name.Equals(values[COURSE]));
                    students.Add(student);
                }
            }

            await CreateLoginsForPersonsAsync(students, cancellationToken);
        }
        
    }
}

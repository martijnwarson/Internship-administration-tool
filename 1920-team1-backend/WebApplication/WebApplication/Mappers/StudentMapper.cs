using System.Threading;
using WebApplication.Dtos;
using WebApplication.Managers;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Mappers.Interfaces;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IStudentMapper{TModel,TDto}"/>
    public class StudentMapper
        : PersonMapper<Student, StudentDto>,
            IStudentMapper
    {
        public StudentMapper(
            IAddressMapper addressMapper)
        {
            AddressMapper = addressMapper;
        }
        
        public IAddressMapper AddressMapper { get; }
        
        public override void OnMap(StudentDto source, Student destination)
        {
            base.OnMap(source, destination);
            destination.Address = AddressMapper.Map(source.Address);
        }

        public override void OnMap(Student source, StudentDto destination)
        {
            base.OnMap(source, destination);
            destination.Address = AddressMapper.Map(source.Address);
            destination.CourseId = source.Course.Id;
        }
    }

}
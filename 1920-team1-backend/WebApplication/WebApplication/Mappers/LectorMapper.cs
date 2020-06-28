using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    public class LectorMapper<TModel, TDto>
        :PersonMapper<TModel, TDto>, 
            ILectorMapper<TModel, TDto>
    where TModel : Lector, new()
    where TDto : LectorDto, new()
    {
        public ICourseMapper CourseMapper { get; }

        public LectorMapper(
            ICourseMapper courseMapper)
        {
            CourseMapper = courseMapper;
        }

        public override void OnMap(TModel source, TDto destination)
        {
            base.OnMap(source, destination);
            destination.CoursesIds = source.Courses.Select(c => c.CourseId).ToArray();
        }
    }
}

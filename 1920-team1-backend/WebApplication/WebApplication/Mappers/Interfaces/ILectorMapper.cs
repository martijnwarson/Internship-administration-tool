using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    public interface ILectorMapper<TModel, TDto>
        : IPersonMapper<TModel, TDto>
        where TModel : Lector
    where TDto : LectorDto

    {
    }
}

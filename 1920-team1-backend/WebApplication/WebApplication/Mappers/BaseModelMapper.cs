using System.Collections.Generic;
using System.Linq;
using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IBidirectionalBaseMapper{TModel,TDto}"/>
    public class BaseModelMapper<TModel, TDto>
        : IBidirectionalBaseMapper<TModel, TDto>
        where TModel : BaseModel, new()
        where TDto : BaseDto, new()
    {
        public virtual TDto Map(TModel model)
        {
            if (model == null)
            {
                return null;
            }
            
            TDto result = new TDto();
            OnMap(model, result);
            return result;
        }

        public virtual TModel Map(TDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            
            TModel result = new TModel();
            OnMap(dto, result);
            return result;
        }

        public virtual void OnMap(TModel source, TDto destination)
        {
            destination.Id = source?.Id;
        }

        public IEnumerable<TDto> Map(IEnumerable<TModel> models)
            => models.Select(Map);

        public virtual void OnMap(TDto source, TModel destination)
        {
            if (source?.Id != null) destination.Id = source.Id.Value;
        }
    }
}
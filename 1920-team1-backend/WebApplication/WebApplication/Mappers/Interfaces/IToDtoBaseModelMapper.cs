using System.Collections;
using System.Collections.Generic;
using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    public interface IToDtoBaseModelMapper<in TModel, TDto>
        where TModel : BaseModel
        where TDto : BaseDto
    {
        /// <summary>
        ///     Method to map from a <see cref="TModel" /> to a <see cref="TDto" />
        /// </summary>
        /// <param name="model">the <see cref="TModel"/> to map</param>
        /// <returns></returns>
        TDto Map(TModel model);

        /// <summary>
        ///     Method to map from <paramref name="source"/> to <paramref name="destination"/>
        /// </summary>
        /// <param name="source">the <see cref="TModel"/> to map</param>
        /// <param name="destination">the <see cref="TDto"/> to map to</param>
        void OnMap(TModel source, TDto destination);
        
        /// <summary>
        ///     Method to map multiple <see cref="TModel"/>s into <see cref="TDto"/> 
        /// </summary>
        /// <param name="models">the <see cref="IEnumerable"/> of <see cref="TModel"/>s.</param>
        /// <returns>an <see cref="IEnumerable"/> of <see cref="TDto"/></returns>
        IEnumerable<TDto> Map(IEnumerable<TModel> models);
    }
}
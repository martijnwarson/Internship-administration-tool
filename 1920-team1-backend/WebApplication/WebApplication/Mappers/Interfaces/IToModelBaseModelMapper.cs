using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Mappers.Interfaces
{
    public interface IToModelBaseModelMapper<TModel, in TDto>
        where TModel : BaseModel
        where TDto : BaseDto
    {
        /// <summary>
        ///     Method to map from a <see cref="TDto" /> to a <see cref="TModel" />
        /// </summary>
        /// <param name="dto">the <see cref="TDto" /> to map</param>
        /// <returns></returns>
        TModel Map(TDto dto);

        /// <summary>
        ///     Method to map from <paramref name="source" /> to <paramref name="destination" />
        /// </summary>
        /// <param name="source">the <see cref="TDto" /> to map</param>
        /// <param name="destination">the <see cref="TModel" /> to map to</param>
        void OnMap(TDto source, TModel destination);
    }
}
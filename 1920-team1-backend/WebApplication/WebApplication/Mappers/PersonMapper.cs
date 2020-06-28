using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IPersonMapper{TModel,TDto}"/>
    public class PersonMapper<TModel, TDto>
        : BaseModelMapper<TModel, TDto>,
            IPersonMapper<TModel, TDto>
    where TModel : Person, new()
    where TDto : Dtos.PersonDto, new()
    {
        public override void OnMap(TDto source, TModel destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
            destination.FirstName = source.FirstName;
            destination.Title = source.Title;
            destination.Email = source.Email;
            destination.TelephoneNumber = source.TelephoneNumber;
        }

        public override void OnMap(TModel source, TDto destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
            destination.FirstName = source.FirstName;
            destination.Title = source.Title;
            destination.Email = source.Email;
            destination.TelephoneNumber = source.TelephoneNumber;
        }
    }
}
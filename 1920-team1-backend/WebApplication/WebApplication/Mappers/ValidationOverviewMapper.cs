using System.Linq;
using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IValidationOverviewMapper"/>
    public class ValidationOverviewMapper
        : BaseModelMapper<Validation, ValidationOverviewDto>,
            IValidationOverviewMapper
    {
        public ValidationOverviewMapper(
            IPersonMapper<Person,PersonDto> personMapper,
            IFeedBackMapper feedBackMapper)
        {
            PersonMapper = personMapper;
            FeedBackMapper = feedBackMapper;
        }

        private IPersonMapper<Person, PersonDto> PersonMapper { get; }
        private IFeedBackMapper FeedBackMapper { get; }

        public override void OnMap(Validation source, ValidationOverviewDto destination)
        {
            base.OnMap(source, destination);
            destination.Date = source.Date;
            destination.Lector = PersonMapper.Map(source.Lector);
            destination.State = source.State;
            destination.FeedBack = FeedBackMapper.Map(source.FeedBack);
        }
    }
}
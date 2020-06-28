using WebApplication.Dtos;
using WebApplication.Enums;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IValidationUpdateMapper"/>
    public class ValidationUpdateMapper
    : BaseModelMapper<Validation, ValidationUpdateDto>,
        IValidationUpdateMapper
    {
        public ValidationUpdateMapper(IFeedBackMapper feedBackMapper)
        {
            FeedBackMapper = feedBackMapper;
        }

        private IFeedBackMapper FeedBackMapper { get; }

        public override void OnMap(ValidationUpdateDto source, Validation destination)
        {
            base.OnMap(source, destination);

            destination.FeedBack = FeedBackMapper.Map(source.Feedback);

            if (destination.State == ValidationStateEnum.NEW)
            {
                destination.State = source.State;
            }
        }
    }
}
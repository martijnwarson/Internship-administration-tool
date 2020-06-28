using WebApplication.Dtos;
using WebApplication.Enums;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IUpdateStateCompanyMapper"/>
    public class UpdateStateCompanyMapper
        : BaseModelMapper<Company, UpdateStateCompanyDto>,
            IUpdateStateCompanyMapper
    {
        public UpdateStateCompanyMapper(IFeedBackMapper feedBackMapper)
        {
            FeedBackMapper = feedBackMapper;
        }

        private IFeedBackMapper FeedBackMapper { get; }
        public override void OnMap(UpdateStateCompanyDto source, Company destination)
        {
            base.OnMap(source, destination);

            destination.FeedBack = FeedBackMapper.Map(source.FeedBack);
        }
    }
}

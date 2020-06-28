using System;
using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IFeedBackMapper"/>
    public class FeedBackMapper : BaseModelMapper<FeedBack, FeedBackDto>, IFeedBackMapper
    {
        public override void OnMap(FeedBack source, FeedBackDto destination)
        {
            base.OnMap(source, destination);
            destination.Value = source.Value;
            if (source.ModifiedAt != null)
            {
                destination.ModifiedAt = source.ModifiedAt.Value;
            }
            
            destination.CreatedAt = source.CreatedAt.Value;
        }

        public override void OnMap(FeedBackDto source, FeedBack destination)
        {
            base.OnMap(source, destination);
            destination.Value = source.Value;
            destination.ModifiedAt ??= DateTime.Now;
            destination.CreatedAt ??= DateTime.Now;
        }
    }
}
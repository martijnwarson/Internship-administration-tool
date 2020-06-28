using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="ITechnologyMapper"/>
    public class TechnologyMapper
    : BaseModelMapper<Technology, TechnologyDto>,
        ITechnologyMapper
    {
        public override void OnMap(Technology source, TechnologyDto destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
        }

        public override void OnMap(TechnologyDto source, Technology destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
        }
    }
}
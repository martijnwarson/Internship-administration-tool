using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IPeriodMapper"/>
    public class PeriodMapper
        : BaseModelMapper<Period, PeriodDto>,
            IPeriodMapper
    {
        public override void OnMap(Period source, PeriodDto destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
            destination.From = source.From;
            destination.To = source.To;
        }

        public override void OnMap(PeriodDto source, Period destination)
        {
            base.OnMap(source, destination);
            destination.Name = source.Name;
            destination.From = source.From;
            destination.To = source.To;
        }
    }
}
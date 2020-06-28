using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IInternshipMapper"/>
    public class InternshipListMapper
        : BaseModelMapper<Internship, InternshipListDto>,
            IInternshipListMapper
    {
        public override void OnMap(Internship source, InternshipListDto destination)
        {
            base.OnMap(source, destination);
            destination.Title = source.Title;
            destination.Company = source.Company.Name;
            destination.State = source.State;
        }
    }
}
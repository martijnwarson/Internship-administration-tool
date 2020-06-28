using System.Security.Cryptography;
using WebApplication.Dtos;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Mappers
{
    /// <inheritdoc cref="IInternshipMapper"/>
    public class InternshipMapper
        : BaseModelMapper<Internship, InternshipDto>,
            IInternshipMapper
    {
        public InternshipMapper(
            IPersonMapper<Person, PersonDto> personMapper,
            IAddressMapper addressMapper,
            IFeedBackMapper feedBackMapper)
        {
            PersonMapper = personMapper;
            AddressMapper = addressMapper;
            FeedBackMapper = feedBackMapper;
        }

        public IPersonMapper<Person, PersonDto> PersonMapper { get; }
        public IAddressMapper AddressMapper { get; }
        public IFeedBackMapper FeedBackMapper { get; }

        public override void OnMap(InternshipDto source, Internship destination)
        {
            base.OnMap(source, destination);
            destination.Title = source.Title;
            destination.Description = source.Description;
            destination.ContactPerson = PersonMapper.Map(source.ContactPerson);
            destination.Address = AddressMapper.Map(source.Address);
            destination.TechDescription = source.Description;
            destination.ResearchTopic = source.ResearchTopic;
            destination.Application = source.Application;
            destination.Résumée = source.Resumee;
            destination.Reimbursement = source.Reimbursement;
            destination.StudentAmount = source.StudentAmount;
            destination.NrOfSupportEmployees = source.NrOfSupportEmployees;
            destination.Remarks = source.Remarks;
            destination.Conditions = source.Conditions;
            destination.FeedBack = FeedBackMapper.Map(source.FeedBack);
        }

        public override void OnMap(Internship source, InternshipDto destination)
        {
            base.OnMap(source, destination);
            destination.Title = source.Title;
            destination.Description = source.Description;
            destination.ContactPerson = PersonMapper.Map(source.ContactPerson);
            destination.Address = AddressMapper.Map(source.Address);
            destination.TechDescription = source.Description;
            destination.ResearchTopic = source.ResearchTopic;
            destination.Application = source.Application;
            destination.Resumee = source.Résumée;
            destination.Reimbursement = source.Reimbursement;
            destination.StudentAmount = source.StudentAmount;
            destination.NrOfSupportEmployees = source.NrOfSupportEmployees;
            destination.Remarks = source.Remarks;
            destination.Conditions = source.Conditions;
            destination.State = source.State;
            destination.CompanyId = source.Company.Id;
            destination.FeedBack = FeedBackMapper.Map(source.FeedBack);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.EmailService;
using WebApplication.EmailTemplates;
using WebApplication.Helpers;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Managers
{
    public class EmailManager : IEmailManager
    {
        public EmailManager(
            ISendGridService sendGridService,
            IPdfGenerator pdfGenerator)
        {
            SendGridService = sendGridService;
            PdfGenerator = pdfGenerator;
        }

        private ISendGridService SendGridService { get; }
        public IPdfGenerator PdfGenerator { get; }

        public async Task SendEmailCompanyApprove(Company company, CancellationToken cancellationToken)
        {
            string body = CompanyApproveTemplate.ComposeBody(company, cancellationToken);
            string mailToName = company.ContactPerson.FirstName + ' ' + company.ContactPerson.Name;
            FileStreamResult attachment = PdfGenerator.CreateCompanyPdf(company);
            await SendGridService.SendMailAsync(body, company.ContactPerson.Email, mailToName, ConstantValues.SUBJECT_DECISION_COMPANY, attachment);
        }

        public async Task SendEmailCompanyDeny(Company company, CancellationToken cancellationToken)
        {
            string body = CompanyDenyTemplate.ComposeBody(company, cancellationToken);
            string mailToName = company.ContactPerson.FirstName + ' ' + company.ContactPerson.Name;
            await SendGridService.SendMailAsync(body, company.ContactPerson.Email, mailToName, ConstantValues.SUBJECT_DECISION_COMPANY);
        }

        public async Task SendEmailInternshipApprove(Internship internship, CancellationToken cancellationToken)
        {
            string body = InternshipApproveTemplate.ComposeBody(internship, cancellationToken);
            string mailToName = internship.ContactPerson.FirstName + ' ' + internship.ContactPerson.Name;
            FileStreamResult attachment = PdfGenerator.CreateInternshipPdf(internship);
            await SendGridService.SendMailAsync(body, internship.ContactPerson.Email, mailToName, ConstantValues.SUBJECT_DECISION_INTERNSHIP, attachment);
        }

        public async Task SendEmailInternshipDeny(Internship internship, CancellationToken cancellationToken)
        {
            string body = InternshipDenyTemplate.ComposeBody(internship, cancellationToken);
            string mailToName = internship.ContactPerson.FirstName + ' ' + internship.ContactPerson.Name;
            await SendGridService.SendMailAsync(body, internship.ContactPerson.Email, mailToName, ConstantValues.SUBJECT_DECISION_INTERNSHIP);
        }

        public async Task SendEmailInternshipFeedback(Internship internship, CancellationToken cancellationToken)
        {
            string body = InternshipFeedbackTemplate.ComposeBody(internship, cancellationToken);
            string mailToName = internship.ContactPerson.FirstName + ' ' + internship.ContactPerson.Name;
            await SendGridService.SendMailAsync(body, internship.ContactPerson.Email, mailToName, ConstantValues.SUBJECT_FEEDBACK_INTERNSHIP);
        }

        public async Task SendEmailLogin(Person person, string password, CancellationToken cancellationToken)
        {
            string body = LoginTemplate.ComposeBody(person, password);
            string mailToName = person.FirstName + ' ' + person.Name;
            await SendGridService.SendMailAsync(body, person.Email, mailToName, ConstantValues.SUBJECT_LOGIN);
        }
    }
}

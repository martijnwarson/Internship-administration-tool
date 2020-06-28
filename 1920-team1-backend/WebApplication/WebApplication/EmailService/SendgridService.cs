using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGridMessage = SendGrid.Helpers.Mail.SendGridMessage;

namespace WebApplication.EmailService
{
    public class SendGridService : ISendGridService
    {
        // Get common values for sending emails from ConstantValues class
        private string sendGridApiKey = ConstantValues.SENDGRID_API_KEY;
        private string mailFrom = ConstantValues.MAIL_FROM;
        private string mailFromName = ConstantValues.MAIL_FROM_NAME;

        public async Task SendMailAsync(string body, string mailTo, string mailToName, string mailSubject)
        {
            var fromMailAddress = new EmailAddress(mailFrom, mailFromName);
            var toMailAddress = new EmailAddress(mailTo, mailToName);
            var subject = mailSubject;

            SendGridMessage msg;

            // Template Mailhelper.CreateSingleEmail: (From, To, subject, plainTextContent, HtmlContent)     
            msg = MailHelper.CreateSingleEmail(fromMailAddress, toMailAddress, subject, null, body);

            Response response = await SendGridMail(msg);
            if (!response.StatusCode.IsSuccessStatusCode())
            {
                throw new Exception($"Er is iets verkeerd gegaan.");
            }
        }
        
        public async Task SendMailAsync(string body, string mailTo, string mailToName, string mailSubject, FileStreamResult attachment)
        {
            var fromMailAddress = new EmailAddress(mailFrom, mailFromName);
            var toMailAddress = new EmailAddress(mailTo, mailToName);
            var subject = mailSubject;

            SendGridMessage msg;

            // Template Mailhelper.CreateSingleEmail: (From, To, subject, plainTextContent, HtmlContent)     
            msg = MailHelper.CreateSingleEmail(fromMailAddress, toMailAddress, subject, null, body);

            if (attachment != null)
            {
                await msg.AddAttachmentAsync("attachment.pdf", attachment.FileStream, attachment.ContentType);
            }
            
            Response response = await SendGridMail(msg);
            if (!response.StatusCode.IsSuccessStatusCode())
            {
                throw new Exception($"Er is iets verkeerd gegaan.");
            }
        }

        private async Task<Response> SendGridMail(SendGridMessage msg)
        {
            var client = new SendGridClient(sendGridApiKey);
            return await client.SendEmailAsync(msg);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Managers.Interfaces
{
    public interface IEmailManager
    {
        /// <summary>
        ///     Send login data to new users
        /// </summary>
        /// <param name="person">The new user</param>
        /// <param name="password">The password for this user</param>
        /// <returns>A task to send an email</returns>
        Task SendEmailLogin(Person person, string password, CancellationToken cancellationToken);

        /// <summary>
        ///     Send an email to inform a company it is approved
        /// </summary>
        /// <param name="company">The company</param>
        /// <returns>A task to send an email</returns>
        Task SendEmailCompanyApprove(Company company, CancellationToken cancellationToken);

        /// <summary>
        ///     Send an email to inform a company it is not approved
        /// </summary>
        /// <param name="company">The company</param>
        /// <returns>A task to send an email</returns>
        Task SendEmailCompanyDeny(Company company, CancellationToken cancellationToken);

        /// <summary>
        ///     Send an email to inform a company the proposal for the internship is approved
        /// </summary>
        /// <param name="internship">The internship</param>
        /// <returns>A task to send an email</returns>
        Task SendEmailInternshipApprove(Internship internship, CancellationToken cancellationToken);

        /// <summary>
        ///      Send an email to inform a company the proposal for the internship is not approved
        /// </summary>
        /// <param name="internship">The internship</param>
        /// <returns>A task to send an email</returns>
        Task SendEmailInternshipDeny(Internship internship, CancellationToken cancellationToken);

        /// <summary>
        ///      Send an email to inform a company there are questions or remarks about the proposal for the internship
        /// </summary>
        /// <param name="internship">The internship</param>
        /// <returns>A task to send an email</returns>
        Task SendEmailInternshipFeedback(Internship internship, CancellationToken cancellationToken);
    }
}

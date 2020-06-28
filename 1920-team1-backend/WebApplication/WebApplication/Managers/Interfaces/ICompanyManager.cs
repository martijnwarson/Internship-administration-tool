using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Managers.Interfaces
{
    /// <inheritdoc cref="IModelManager{TModel}"/>
    public interface ICompanyManager
        : IModelManager<Company>
    {
        /// <summary>
        ///      Method to get the <see cref="Company"/> of the given <paramref name="contactPerson"/> 
        /// </summary>
        /// <param name="contactPerson">the given <see cref="Person"/></param>
        /// <param name="cancellationToken"> token to cancel the request</param>
        /// <returns>a <see cref="Task"/> to get a <see cref="Company"/></returns>
        Task<Company> GetCompanyByContactAsync(Person contactPerson, CancellationToken cancellationToken);
        
        /// <summary>
        ///     Method to get a list of <see cref="Company"/> filtered by state/>
        /// </summary>
        /// <param name="company">the given <see cref="Company"/></param>
        /// <param name="cancellationToken"> token to cancel the request</param>
        /// <returns>a <see cref="Task"/></returns>
        Task<ISet<Company>> GetAllByStateAsync (CompanyStateEnum stateEnum, CancellationToken cancellationToken);

        /// <summary>
        ///     Activate the given <see cref="Company"/>.
        ///     Generating a password for the ContactPerson of the <see cref="Company"/>.
        /// </summary>
        /// <param name="company">the given <see cref="Company"/></param>
        /// <param name="cancellationToken"> token to cancel the request</param>
        /// <returns>a <see cref="Task"/> returning username and password</returns>
        Task<(string username, string password)> ActivateCompanyAsync(Company company, CancellationToken cancellationToken);

        /// <summary>
        ///     Deactivate the given <see cref="Company"/>
        /// </summary>
        /// <param name="company">the given <see cref="Company"/></param>
        /// <param name="cancellationToken"> token to cancel the request</param>
        /// <returns>a <see cref="Task"/></returns>
        Task DeactivateCompanyAsync(Company company, CancellationToken cancellationToken);

        /// <summary>
        ///     Create PDF of the company for the logged in Contact Person <see cref="Company"/>
        /// </summary>
        /// <param name="userId">id of the logged in user</param>
        /// <param name="cancellationToken"> token to cancel the request</param>
        /// <returns>a <see cref="Task"/>returning a FileStreamResult containing the created PDF</returns>
        Task<FileStreamResult> GetCompanyPdfAsync(long userId, CancellationToken cancellationToken);
    }
    
}
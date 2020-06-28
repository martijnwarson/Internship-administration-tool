using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    /// <inheritdoc cref="ICrudRepository{TModel}" />
    public interface ICompanyRepository
        : ICrudRepository<Company>
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
        /// <param name="stateEnum">the given <see cref="CompanyStateEnum"/>/>/></param>
        /// <param name="cancellationToken"> token to cancel the request</param>
        /// <returns>a <see cref="Task"/></returns>
        Task<IList<Company>> GetAllByStateAsync(CompanyStateEnum stateEnum, CancellationToken cancellationToken);
    }
}
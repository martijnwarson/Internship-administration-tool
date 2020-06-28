using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    /// <inheritdoc cref="IAddressRepository" />
    public class AddressRepository
        : CrudRepository<Address>,
            IAddressRepository
    {
        public AddressRepository(DataContext dataContext)
            : base(dataContext)
        {
        }
    }
}
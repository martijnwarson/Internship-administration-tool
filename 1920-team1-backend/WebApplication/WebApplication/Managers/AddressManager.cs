using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="IAddressManager" />
    public class AddressManager
        : ModelManager<Address>,
            IAddressManager
    {
        public AddressManager(ICrudRepository<Address> repository) : base(repository)
        {
        }
    }
}
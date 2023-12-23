using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Models;

namespace Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        internal CustomerRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

      
    }
}

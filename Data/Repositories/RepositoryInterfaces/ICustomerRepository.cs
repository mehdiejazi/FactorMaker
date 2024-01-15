using Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface ICustomerRepository : Data.Base.IRepository<Customer>
    {
        Task<ICollection<Customer>> GetTop10CustomersByQuantityAsync(Guid storeId);
        Task<ICollection<Customer>> GetTop10CustomersByPriceAsync(Guid storeId);
    }
}

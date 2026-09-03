using Data.DataTransferObjects.Customer;
using Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces{
    public interface ICustomerRepository : Data.Base.IRepository<Customer>
    {
        Task<ICollection<CustomerSaleTotalQuantityDto>> GetTop10CustomersByQuantityAsync(Guid storeId);
        Task<ICollection<CustomerSaleTotalPriceDto>> GetTop10CustomersByPriceAsync(Guid storeId);
        Task<ICollection<Customer>> GetByStoreIdAsync(Guid storeId);
        Task<ICollection<CustomerGenderStatDto>> GetGenderStatsByStoreIdAsync(Guid storeId);
    }
}

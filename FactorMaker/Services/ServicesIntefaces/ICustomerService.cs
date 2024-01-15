using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Customer;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface ICustomerService
    {
        Task<Result<CustomerViewModel>> InsertAsync(CustomerViewModel viewModel);
        Task<Result<CustomerViewModel>> UpdateAsync(CustomerViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<CustomerViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<CustomerViewModel>>> GetAllAsync();
        Task<Result<ICollection<CustomerViewModel>>> GetTop10ByQuantityAsync(Guid storeId);
        Task<Result<ICollection<CustomerViewModel>>> GetTop10ByPriceAsync(Guid storeId);
       
    }
}
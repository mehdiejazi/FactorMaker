using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Customer;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface ICustomerService
    {
        Task<Result<CustomerViewModel>> InsertAsync(CustomerViewModel viewModel);
        Task<Result<CustomerViewModel>> UpdateAsync(CustomerViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<CustomerViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<CustomerViewModel>>> GetAllAsync();
        Task<Result<ICollection<CustomerViewModel>>> GetTop10ByQuantityAsync(User user,Guid storeId);
        Task<Result<ICollection<CustomerViewModel>>> GetTop10ByPriceAsync(User user,Guid storeId);
        Task<Result<ICollection<CustomerViewModel>>> GetByStoreIdAsync(User user, Guid storeId);


    }
}
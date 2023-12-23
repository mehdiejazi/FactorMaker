using Common;
using Mapster;
using Models;
using Resources;
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
    }
}
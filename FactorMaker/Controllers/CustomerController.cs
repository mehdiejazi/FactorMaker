using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Customer;

namespace FactorMaker.Controllers
{
    [Authorize]
    public class CustomerController : BaseApiController
    {
        private CustomerController() : base()
        {

        }
        public CustomerController(ICustomerService customerService)
        {
            CustomerService = customerService;
        }
        private ICustomerService CustomerService { get; }

        [HttpGet("GetAllAsync")]
        public async Task<Result<ICollection<CustomerViewModel>>> GetAllAsync()
        {
            var result =  await CustomerService.GetAllAsync();
            return result;
        }
        
        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var result = await CustomerService.DeleteByIdAsync(id);
            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<CustomerViewModel>> GetByIdAsync(Guid id)
        {
            var result = await CustomerService.GetByIdAsync(id);
            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<CustomerViewModel>> InsertAsync(CustomerViewModel viewModel)
        {
            var result = await CustomerService.InsertAsync(viewModel);
            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<CustomerViewModel>> UpdateAsync(CustomerViewModel viewModel)
        {
            var result =  await CustomerService.UpdateAsync(viewModel);
            return result;
        }
    }
}

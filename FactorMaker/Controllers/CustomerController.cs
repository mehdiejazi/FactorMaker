﻿using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Customer;

namespace FactorMaker.Controllers
{
    //[Authorize]
    public class CustomerController : BaseApiControllerWithUser
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
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await CustomerService.GetAllAsync();
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await CustomerService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await CustomerService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(CustomerViewModel viewModel)
        {
            var result = await CustomerService.InsertAsync(viewModel);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(CustomerViewModel viewModel)
        {
            var result = await CustomerService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpPost("GetTop10ByQuantityAsync")]
        public async Task<IActionResult> GetTop10ByQuantityAsync(Guid storeId)
        {
            var result = await CustomerService.GetTop10ByQuantityAsync(User, storeId);
            return Result(result);
        }

        [HttpPost("GetTop10ByPriceAsync")]
        public async Task<IActionResult> GetTop10ByPriceAsync(Guid storeId)
        {
            var result = await CustomerService.GetTop10ByPriceAsync(User,storeId);
            return Result(result);
        }

        [HttpGet("GetByStoreIdAsync")]
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            var result = await CustomerService.GetByStoreIdAsync(User, storeId);
            return Result(result);
        }

    }
}

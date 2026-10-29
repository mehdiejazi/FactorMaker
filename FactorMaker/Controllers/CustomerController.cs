using FactorMaker.Infrastructure;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Customer;

namespace FactorMaker.Controllers
{
    [Authenticated]
    public class CustomerController : BaseApiControllerWithUser
    {
        private CustomerController() : base()
        {

        }
        public CustomerController(ICustomerService customerService, AccessGuard accessGuard)
        {
            CustomerService = customerService;
            AccessGuard = accessGuard;
        }
        private ICustomerService CustomerService { get; }
        private AccessGuard AccessGuard { get; }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await CustomerService.GetAllAsync();
            await AccessGuard.FilterByStoreAsync(User, result, x => x.StoreId);
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessCustomerAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await CustomerService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessCustomerAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await CustomerService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(CustomerViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, viewModel.StoreId)) return Result(AccessGuard.Denied());
            var result = await CustomerService.InsertAsync(viewModel);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(CustomerViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessCustomerAsync(User, viewModel.Id) ||
                !await AccessGuard.CanAccessStoreAsync(User, viewModel.StoreId)) return Result(AccessGuard.Denied());
            var result = await CustomerService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpPost("GetTop10ByQuantityAsync")]
        public async Task<IActionResult> GetTop10ByQuantityAsync(Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await CustomerService.GetTop10ByQuantityAsync(User, storeId);
            return Result(result);
        }

        [HttpPost("GetTop10ByPriceAsync")]
        public async Task<IActionResult> GetTop10ByPriceAsync(Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await CustomerService.GetTop10ByPriceAsync(User, storeId);
            return Result(result);
        }

        [HttpGet("GetGenderStatsByStoreIdAsync")]
        public async Task<IActionResult> GetGenderStatsByStoreIdAsync(Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await CustomerService.GetGenderStatsByStoreIdAsync(User, storeId);
            return Result(result);
        }

        [HttpGet("GetByStoreIdAsync")]
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await CustomerService.GetByStoreIdAsync(User, storeId);
            return Result(result);
        }
    }
}
using FactorMaker.Infrastructure;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Factor;

namespace FactorMaker.Controllers
{
    [Authenticated]
    public class FactorController : BaseApiControllerWithUser
    {
        private FactorController() : base()
        {

        }
        public FactorController(IFactorService factorService, AccessGuard accessGuard)
        {
            FactorService = factorService;
            AccessGuard = accessGuard;
        }
        private IFactorService FactorService { get; }
        private AccessGuard AccessGuard { get; }

        [HttpGet("GetByOwnerIdAsync")]
        public async Task<IActionResult> GetByOwnerIdAsync(Guid ownerId)
        {
            if (!await AccessGuard.CanAccessCustomerAsync(User, ownerId)) return Result(AccessGuard.Denied());
            var result = await FactorService.GetByOwnerIdAsync(ownerId);
            await AccessGuard.FilterByStoreAsync(User, result, x => x.StoreId);
            return Result(result);
        }

        [HttpGet("GetByStoreIdAsync")]
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await FactorService.GetByStoreIdAsync(User, storeId);
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessFactorAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await FactorService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessFactorAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await FactorService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetFactorWithItemsByIdAsync")]
        public async Task<IActionResult> GetFactorWithItemsByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessFactorAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await FactorService.GetFactorWithItemsByIdAsync(User, id);
            return Result(result);
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(FactorViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, viewModel.StoreId) ||
                !await AccessGuard.CanAccessCustomerAsync(User, viewModel.OwnerId)) return Result(AccessGuard.Denied());
            var result = await FactorService.InsertAsync(viewModel);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(FactorViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessFactorAsync(User, viewModel.Id) ||
                !await AccessGuard.CanAccessStoreAsync(User, viewModel.StoreId) ||
                !await AccessGuard.CanAccessCustomerAsync(User, viewModel.OwnerId)) return Result(AccessGuard.Denied());
            var result = await FactorService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpGet("CalculateFactorByIdAsync")]
        public async Task<IActionResult> CalculateFactorByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessFactorAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await FactorService.CalculateFactorByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetMonthlyFactorSaleAsync")]
        public async Task<IActionResult> GetMonthlyFactorSaleAsync(int year, Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await FactorService.GetMonthlyFactorSaleAsync(User, year, storeId);
            return Result(result);
        }

        [HttpGet("GetWeeklyFactorSaleAsync")]
        public async Task<IActionResult> GetWeeklyFactorSaleAsync(int year, int month, Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await FactorService.GetWeeklyFactorSaleAsync(User, year, month, storeId);
            return Result(result);
        }

        [HttpGet("GetHourlyFactorSaleAsync")]
        public async Task<IActionResult> GetHourlyFactorSaleAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await FactorService.GetHourlyFactorSaleAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }

        [HttpGet("GetWeekDailyFactorSaleAsync")]
        public async Task<IActionResult> GetWeekDailyFactorSaleAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await FactorService.GetWeekDailyFactorSaleAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }
    }
}
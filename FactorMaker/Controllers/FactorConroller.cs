using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Factor;

namespace FactorMaker.Controllers
{
    [Authorize]

    public class FactorConroller : BaseApiControllerWithUser
    {
        private FactorConroller() : base()
        {

        }
        public FactorConroller(IFactorService factorService)
        {
            FactorService = factorService;
        }
        private IFactorService FactorService { get; }

        [HttpGet("GetByOwnerIdAsync")]
        async Task<IActionResult> GetByOwnerIdAsync(Guid ownerId)
        {
            var result = await FactorService.GetByOwnerIdAsync(ownerId);
            return Result(result);
        }

        [HttpGet("GetByStoreIdAsync")]
        async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            var result = await FactorService.GetByStoreIdAsync(User, storeId);
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await FactorService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await FactorService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(FactorViewModel viewModel)
        {
            var result = await FactorService.InsertAsync(viewModel);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(FactorViewModel viewModel)
        {
            var result = await FactorService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpGet("CalculateFactorByIdAsync")]
        public async Task<IActionResult> CalculateFactorByIdAsync(Guid id)
        {
            var result = await FactorService.CalculateFactorByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetMonthlyFactorSaleAsync")]
        public async Task<IActionResult> GetMonthlyFactorSaleAsync(int year, Guid storeId)
        {
            var result = await FactorService.GetMonthlyFactorSaleAsync(User, year, storeId);
            return Result(result);
        }

        [HttpGet("GetWeeklyFactorSaleAsync")]
        public async Task<IActionResult> GetWeeklyFactorSaleAsync(int year, int month, Guid storeId)
        {
            var result = await FactorService.GetWeeklyFactorSaleAsync(User, year, month, storeId);
            return Result(result);
        }

        [HttpGet("GetWeeklyFactorSaleAsync")]
        public async Task<IActionResult> GetHourlyFactorSaleAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var result = await FactorService.GetHourlyFactorSaleAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }

        [HttpGet("GetWeekDailyFactorSaleAsync")]
        public async Task<IActionResult> GetWeekDailyFactorSaleAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var result = await FactorService.GetWeekDailyFactorSaleAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }
    }
}

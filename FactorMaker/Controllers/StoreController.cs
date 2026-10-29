using FactorMaker.Infrastructure;
using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Store;

namespace FactorMaker.Controllers
{
    public class StoreController : BaseApiControllerWithUser
    {
        private StoreController() : base()
        {

        }
        public StoreController(IStoreService storeService, AccessGuard accessGuard)
        {
            StoreService = storeService;
            AccessGuard = accessGuard;
        }
        private IStoreService StoreService   
        { get; }
        private AccessGuard AccessGuard { get; }

        [Authorize]
        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(StoreViewModel viewModel)
        {
            viewModel.OwnerId = User.Id;
            var result = await StoreService.InsertAsync(viewModel);
            return Result(result);
        }

        [Authorize]
        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(StoreViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessStoreByIdAsync(User, viewModel.Id)) return Result(AccessGuard.Denied());
            var result = await StoreService.UpdateAsync(viewModel);
            return Result(result);
        }

        [Authorize]
        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessStoreByIdAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await StoreService.DeleteByIdAsync(id);
            return Result(result);
        }

        [Authorize]
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessStoreByIdAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await StoreService.GetByIdAsync(id);
            return Result(result);
        }

        [Authorize]
        [HttpGet("GetByOwnerAsync")]
        public async Task<IActionResult> GetByOwnerAsync()
        {
            var result = await StoreService.GetByOwnerIdAsync(User.Id);
            return Result(result);
        }

        [HttpGet("GetActiveAsync")]
        public async Task<IActionResult> GetActiveAsync()
        {
            var result = await StoreService.GetActiveAsync();
            return Result(result);
        }

        [HttpGet("GetByStoreEnglishNameAsync")]
        public async Task<IActionResult> GetByStoreEnglishNameAsync(string storeEnglishName)
        {
            var result = await StoreService.GetByStoreEnglishNameAsync(storeEnglishName);
            return Result(result);
        }

    }
}

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
        public StoreController(IStoreService storeService)
        {
            StoreService = storeService;
        }
        private IStoreService StoreService   
        { get; }

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
            var result = await StoreService.UpdateAsync(viewModel);
            return Result(result);
        }

        [Authorize]
        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await StoreService.DeleteByIdAsync(id);
            return Result(result);
        }

        [Authorize]
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
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

        [Authorize]
        [HttpGet("GetByStoreEnglishNameAsync")]
        public async Task<IActionResult> GetByStoreEnglishNameAsync(string storeEnglishName)
        {
            var result = await StoreService.GetByStoreEnglishNameAsync(storeEnglishName);
            return Result(result);
        }

    }
}

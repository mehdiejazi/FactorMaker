using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Store;

namespace FactorMaker.Controllers
{
    public class StoreConroller : BaseApiControllerWithUser
    {
        private StoreConroller() : base()
        {

        }
        public StoreConroller(IStoreServuce storeService)
        {
            StoreServuce = storeService;
        }
        private IStoreServuce StoreServuce { get; }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(StoreViewModel viewModel)
        {
            var result = await StoreServuce.InsertAsync(viewModel);
            viewModel.OwnerId = User.Id;
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(StoreViewModel viewModel)
        {
            var result = await StoreServuce.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await StoreServuce.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await StoreServuce.GetByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByOwnerIdAsync")]
        public async Task<IActionResult> GetByOwnerAsync()
        {
            var result = await StoreServuce.GetByOwnerIdAsync(User.Id);
            return Result(result);
        }

        [HttpGet("GetByStoreIdAsync")]
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            var result = await StoreServuce.GetByStoreIdAsync(storeId);
            return Result(result);
        }

    }
}

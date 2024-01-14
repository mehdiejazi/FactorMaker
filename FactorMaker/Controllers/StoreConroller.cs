using Common;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ViewModels.Store;
using Microsoft.AspNetCore.Mvc;

namespace FactorMaker.Controllers
{
    public class StoreConroller : BaseApiController
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
        public async Task<IActionResult> GetByOwnerIdAsync(Guid ownerId)
        {
            var result = await StoreServuce.GetByOwnerIdAsync(ownerId);
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

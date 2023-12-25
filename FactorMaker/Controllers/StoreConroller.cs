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
        public async Task<Result<StoreViewModel>> InsertAsync(StoreViewModel viewModel)
        {
            var result = await StoreServuce.InsertAsync(viewModel);
            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<StoreViewModel>> UpdateAsync(StoreViewModel viewModel)
        {
            var result = await StoreServuce.UpdateAsync(viewModel);
            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var result = await StoreServuce.DeleteByIdAsync(id);
            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<StoreViewModel>> GetByIdAsync(Guid id)
        {
            var result = await StoreServuce.GetByIdAsync(id);
            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<ICollection<StoreViewModel>>> GetByOwnerIdAsync(Guid ownerId)
        {
            var result = await StoreServuce.GetByOwnerIdAsync(ownerId);
            return result;
        }

        [HttpGet("GetByStoreIdAsync")]
        public async Task<Result<StoreViewModel>> GetByStoreIdAsync(Guid storeId)
        {
            var result = await StoreServuce.GetByStoreIdAsync(storeId);
            return result;
        }

    }
}

using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Factor;

namespace FactorMaker.Controllers
{
    [Authorize]

    public class FactorConroller : BaseApiController
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
        async Task<Result<ICollection<FactorViewModel>>> GetByOwnerIdAsync(Guid ownerId)
        {
            var result = await FactorService.GetByOwnerIdAsync(ownerId);
            return result;
        }

        [HttpGet("GetByCreatorIdAsync")]
        async Task<Result<ICollection<FactorViewModel>>> GetByCreatorIdAsync(Guid creatorId)
        {
            var result = await FactorService.GetByCreatorIdAsync(creatorId);
            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var result = await FactorService.DeleteByIdAsync(id);
            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<FactorViewModel>> GetByIdAsync(Guid id)
        {
            var result = await FactorService.GetByIdAsync(id);
            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<FactorViewModel>> InsertAsync(FactorViewModel viewModel)
        {
            var result = await FactorService.InsertAsync(viewModel);
            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<FactorViewModel>> UpdateAsync(FactorViewModel viewModel)
        {
            var result = await FactorService.UpdateAsync(viewModel);
            return result;
        }

        [HttpGet("CalculateFactorByIdAsync")]
        public async Task<Result<TotalFactorViewModel>> CalculateFactorByIdAsync(Guid id)
        {
            var result = await FactorService.CalculateFactorByIdAsync(id);
            return result;
        }
    }
}

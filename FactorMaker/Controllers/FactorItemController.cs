using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace FactorMaker.Controllers
{
    [Authorize]
    public class FactorItemController : BaseApiController
    {
        private FactorItemController() : base()
        {

        }
        public FactorItemController(IFactorItemService factorItemService)
        {
            FactorItemService = factorItemService;
        }
        private IFactorItemService FactorItemService { get; }

        [HttpGet("DeleteById")]
        public Result DeleteById(Guid id)
        {
            FactorItemService.DeleteById(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            await FactorItemService.DeleteByIdAsync(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetById")]
        public Result<FactorItem> GetById(Guid id)
        {
            var result = new Result<FactorItem>();
            result.Data = FactorItemService.GetById(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<FactorItem>> GetByIdAsync(Guid id)
        {
            var result = new Result<FactorItem>();
            result.Data = await FactorItemService.GetByIdAsync(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Update")]
        public Result<FactorItem> Update(FactorItemViewModel factorItemViewModel)
        {
            var result = new Result<FactorItem>();
            result.Data = FactorItemService.Update(factorItemViewModel.Id, factorItemViewModel.productId,
                            factorItemViewModel.quantity, factorItemViewModel.offpercent);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<FactorItem>> UpdateAsync(FactorItemViewModel factorItemViewModel)
        {
            var result = new Result<FactorItem>();
            result.Data = await FactorItemService.UpdateAsync(factorItemViewModel.Id, factorItemViewModel.productId,
                            factorItemViewModel.quantity, factorItemViewModel.offpercent);
            result.IsSuccessful = true;

            return result;
        }

    }
}

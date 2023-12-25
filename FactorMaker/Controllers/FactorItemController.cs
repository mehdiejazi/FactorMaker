using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.FactorItem;

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

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var result = await FactorItemService.DeleteByIdAsync(id);
            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<FactorItemViewModel>> GetByIdAsync(Guid id)
        {
            var result =  await FactorItemService.GetByIdAsync(id);
            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<FactorItemViewModel>> UpdateAsync(FactorItemViewModel viewModel)
        {
            var result = await FactorItemService.UpdateAsync(viewModel);
            return result;
        }

    }
}

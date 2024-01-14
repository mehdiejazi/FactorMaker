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
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await FactorItemService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await FactorItemService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(FactorItemViewModel viewModel)
        {
            var result = await FactorItemService.UpdateAsync(viewModel);
            return Result(result);
        }

    }
}

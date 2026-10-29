using FactorMaker.Infrastructure;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.FactorItem;

namespace FactorMaker.Controllers
{
    [Authorize]
    public class FactorItemController : BaseApiControllerWithUser
    {
        private FactorItemController() : base()
        {

        }
        public FactorItemController(IFactorItemService factorItemService, AccessGuard accessGuard)
        {
            FactorItemService = factorItemService;
            AccessGuard = accessGuard;
        }
        private IFactorItemService FactorItemService { get; }
        private AccessGuard AccessGuard { get; }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessFactorItemAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await FactorItemService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessFactorItemAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await FactorItemService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(FactorItemViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessFactorItemAsync(User, viewModel.Id) ||
                !await AccessGuard.CanAccessFactorAsync(User, viewModel.FactorId) ||
                !await AccessGuard.CanAccessProductAsync(User, viewModel.ProductId)) return Result(AccessGuard.Denied());
            var result = await FactorItemService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(FactorItemViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessFactorAsync(User, viewModel.FactorId) ||
                !await AccessGuard.CanAccessProductAsync(User, viewModel.ProductId)) return Result(AccessGuard.Denied());
            var result = await FactorItemService.InsertAsync(viewModel);
            return Result(result);
        }
    }
}
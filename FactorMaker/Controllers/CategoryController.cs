using FactorMaker.Infrastructure;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Category;

namespace FactorMaker.Controllers
{
    [Authenticated]
    public class CategoryController : BaseApiControllerWithUser
    {
        private CategoryController() : base()
        {

        }
        public CategoryController(ICategoryService categoryService, AccessGuard accessGuard)
        {
            CategoryService = categoryService;
            AccessGuard = accessGuard;
        }
        private ICategoryService CategoryService { get; }
        private AccessGuard AccessGuard { get; }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(CategoryViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, viewModel.StoreId)) return Result(AccessGuard.Denied());
            var result = await CategoryService.InsertAsync(viewModel);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(CategoryViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessCategoryAsync(User, viewModel.Id) ||
                !await AccessGuard.CanAccessStoreAsync(User, viewModel.StoreId)) return Result(AccessGuard.Denied());
            var result = await CategoryService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessCategoryAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await CategoryService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessCategoryAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await CategoryService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await CategoryService.GetAllAsync();
            await AccessGuard.FilterByStoreAsync(User, result, x => x.StoreId);
            return Result(result);
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("GetByStoreIdAsync")]
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            var result = await CategoryService.GetByStoreIdAsync(storeId);
            return Result(result);
        }

        [HttpGet("GetSaleTotalByPriceAsync")]
        public async Task<IActionResult> GetSaleTotalByPriceAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await CategoryService.GetSaleTotalByPriceAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }

        [HttpGet("GetSaleTotalQuantityAsync")]
        public async Task<IActionResult> GetSaleTotalQuantityAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await CategoryService.GetSaleTotalQuantityAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }
    }
}
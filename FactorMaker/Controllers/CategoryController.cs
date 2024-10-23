using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Category;

namespace FactorMaker.Controllers
{
    public class CategoryController : BaseApiControllerWithUser
    {
        private CategoryController() : base()
        {

        }
        public CategoryController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }
        private ICategoryService CategoryService { get; }


        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(CategoryViewModel viewModel)
        {
            var result = await CategoryService.InsertAsync(viewModel);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(CategoryViewModel viewModel)
        {
            var result = await CategoryService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await CategoryService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await CategoryService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await CategoryService.GetAllAsync();
            return Result(result);
        }

        [HttpGet("GetByStoreIdAsync")]
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            var result = await CategoryService.GetByStoreIdAsync(storeId);
            return Result(result);
        }

        [HttpGet("GetSaleTotalByPriceAsync")]
        public async Task<IActionResult> GetSaleTotalByPriceAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var result = await CategoryService.GetSaleTotalByPriceAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }

        [HttpGet("GetSaleTotalQuantityAsync")]
        public async Task<IActionResult> GetSaleTotalQuantityAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var result = await CategoryService.GetSaleTotalQuantityAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }

    }
}

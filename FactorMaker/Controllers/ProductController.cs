using FactorMaker.Infrastructure;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Product;

namespace FactorMaker.Controllers
{
    [Authenticated]
    public class ProductController : BaseApiControllerWithUser
    {
        private ProductController() : base()
        {

        }
        public ProductController(IProductService productService, AccessGuard accessGuard)
        {
            ProductService = productService;
            AccessGuard = accessGuard;
        }
        private IProductService ProductService { get; }
        private AccessGuard AccessGuard { get; }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await ProductService.GetAllAsync();
            await AccessGuard.FilterByStoreAsync(User, result, x => x.StoreId);
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessProductAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await ProductService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessProductAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await ProductService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(ProductViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, viewModel.StoreId) ||
                !await AccessGuard.CanAccessCategoryAsync(User, viewModel.CategoryId)) return Result(AccessGuard.Denied());
            var result = await ProductService.InsertAsync(viewModel);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(ProductViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessProductAsync(User, viewModel.Id) ||
                !await AccessGuard.CanAccessStoreAsync(User, viewModel.StoreId) ||
                !await AccessGuard.CanAccessCategoryAsync(User, viewModel.CategoryId)) return Result(AccessGuard.Denied());
            var result = await ProductService.UpdateAsync(viewModel);
            return Result(result);
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("GetByStoreIdAsync")]
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            var result = await ProductService.GetByStoreIdAsync(storeId);
            return Result(result);
        }

        [HttpGet("GetByStoreIdCategoryIdAsync")]
        public async Task<IActionResult> GetByStoreIdCategoryIdAsync(Guid storeId, Guid categoryId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await ProductService.GetByStoreIdCategoryIdAsync(storeId, categoryId);
            return Result(result);
        }

        [HttpGet("GetTop10SaleByQuantityAsync")]
        public async Task<IActionResult> GetTop10SaleByQuantityAsync(Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await ProductService.GetTop10SaleByQuantityAsync(User, storeId);
            return Result(result);
        }

        [HttpGet("GetTop10SaleByPriceAsync")]
        public async Task<IActionResult> GetTop10SaleByPriceAsync(Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await ProductService.GetTop10SaleByPriceAsync(User, storeId);
            return Result(result);
        }

        [HttpGet("GetSaleTotalByQuantityAsync")]
        public async Task<IActionResult> GetSaleTotalByQuantityAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await ProductService.GetSaleTotalByQuantityAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }

        [HttpGet("GetSaleTotalByPriceAsync")]
        public async Task<IActionResult> GetSaleTotalByPriceAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            if (!await AccessGuard.CanAccessStoreAsync(User, storeId)) return Result(AccessGuard.Denied());
            var result = await ProductService.GetSaleTotalByPriceAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }
    }
}
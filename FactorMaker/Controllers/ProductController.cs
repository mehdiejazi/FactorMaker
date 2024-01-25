using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Product;

namespace FactorMaker.Controllers
{
    //[Authorize]
    public class ProductController : BaseApiControllerWithUser
    {
        private ProductController() : base()
        {

        }
        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }
        private IProductService ProductService { get; }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await ProductService.GetAllAsync();
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await ProductService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await ProductService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(ProductViewModel viewModel)
        {
            var result = await ProductService.InsertAsync(viewModel);
            viewModel.OwnerId = User.Id;
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(ProductViewModel viewModel)
        {
            var result = await ProductService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpGet("GetTop10SaleByQuantityAsync")]
        public async Task<IActionResult> GetTop10SaleByQuantityAsync(Guid storeId)
        {
            var result = await ProductService.GetTop10SaleByQuantityAsync(User, storeId);
            return Result(result);

        }

        [HttpGet("GetTop10SaleByPriceAsync")]
        public async Task<IActionResult> GetTop10SaleByPriceAsync(Guid storeId)
        {
            var result = await ProductService.GetTop10SaleByPriceAsync(User, storeId);
            return Result(result);
        }

        [HttpGet("GetSaleTotalByQuantityAsync")]
        public async Task<IActionResult> GetSaleTotalByQuantityAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var result = await ProductService.GetSaleTotalByQuantityAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }

        [HttpGet("GetSaleTotalByPriceAsync")]
        public async Task<IActionResult> GetSaleTotalByPriceAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var result = await ProductService.GetSaleTotalByPriceAsync(User, dtFrom, dtTo, storeId);
            return Result(result);
        }
    }
}

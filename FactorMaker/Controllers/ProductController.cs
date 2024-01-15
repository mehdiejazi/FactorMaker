using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Product;

namespace FactorMaker.Controllers
{
    //[Authorize]
    public class ProductController : BaseApiController
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
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(ProductViewModel viewModel)
        {
            var result = await ProductService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpGet("GetTop10SellingByQuantityAsync")]
        public async Task<IActionResult> GetTop10SellingByQuantityAsync(Guid storeId)
        {
            var result = await ProductService.GetTop10SaleByQuantityAsync(storeId);
            return Result(result);

        }

        [HttpGet("GetTop10SellingByPriceAsync")]
        public async Task<IActionResult> GetTop10SellingByPriceAsync(Guid storeId)
        {
            var result = await ProductService.GetTop10SaleByPriceAsync(storeId);
            return Result(result);
        }

    }
}

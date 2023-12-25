using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Product;

namespace FactorMaker.Controllers
{
    [Authorize]
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
        public async Task<Result<ICollection<ProductViewModel>>> GetAllAsync()
        {
            var result = await ProductService.GetAllAsync();
            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var result = await ProductService.DeleteByIdAsync(id);
            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<ProductViewModel>> GetByIdAsync(Guid id)
        {
            var result =  await ProductService.GetByIdAsync(id);
            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<ProductViewModel>> InsertAsync(ProductViewModel viewModel)
        {
            var result = await ProductService.InsertAsync(viewModel);
            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<ProductViewModel>> UpdateAsync(ProductViewModel viewModel)
        {
            var result =  await ProductService.UpdateAsync(viewModel);
            return result;
        }

    }
}

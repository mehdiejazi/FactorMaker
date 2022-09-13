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
    [Authorize()]
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

        [HttpGet("DeleteById")]
        public Result DeleteById(Guid id)
        {
            ProductService.DeleteById(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            await ProductService.DeleteByIdAsync(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetAll")]
        public Result<ICollection<Product>> GetAll()
        {
            var result = new Result<ICollection<Product>>();
            result.Data = ProductService.GetAll();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetAllAsync")]
        public async Task<Result<ICollection<Product>>> GetAllAsync()
        {
            var result = new Result<ICollection<Product>>();
            result.Data = await ProductService.GetAllAsync();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetById")]
        public Result<Product> GetById(Guid id)
        {
            var result = new Result<Product>();
            result.Data = ProductService.GetById(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<Product>> GetByIdAsync(Guid id)
        {
            var result = new Result<Product>();
            result.Data = await ProductService.GetByIdAsync(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Insert")]
        public Result<Product> Insert(ProductViewModel productViewModel)
        {
            var result = new Result<Product>();
            result.Data = ProductService.Insert(productViewModel.Name, productViewModel.Price);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<Product>> InsertAsync(ProductViewModel productViewModel)
        {
            var result = new Result<Product>();
            result.Data = await ProductService.InsertAsync(productViewModel.Name, productViewModel.Price);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Update")]
        public Result<Product> Update(ProductViewModel productViewModel)
        {
            var result = new Result<Product>();
            result.Data = ProductService.Update(productViewModel.Id, productViewModel.Name,productViewModel.Price);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<Product>> UpdateAsync(ProductViewModel productViewModel)
        {
            var result = new Result<Product>();
            result.Data = await ProductService.UpdateAsync(productViewModel.Id, productViewModel.Name, productViewModel.Price);
            result.IsSuccessful = true;

            return result;
        }
   
    }
}

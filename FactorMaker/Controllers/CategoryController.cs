using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactorMaker.Controllers
{
    public class CategoryController : BaseApiController
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

        [HttpGet("GetByOwnerIdAsync")]
        public async Task<IActionResult> GetByOwnerIdAsync(Guid ownerId)
        {
            var result = await CategoryService.GetAllAsync();
            return Result(result);
        }

    }
}

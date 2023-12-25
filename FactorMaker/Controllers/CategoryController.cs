using Common;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;

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
        public Task<Result<CategoryViewModel>> InsertAsync(CategoryViewModel viewModel)
        {
            var result = CategoryService.InsertAsync(viewModel);
            return result;
        }

        [HttpPost("UpdateAsync")]
        public Task<Result<CategoryViewModel>> UpdateAsync(CategoryViewModel viewModel)
        {
            var result = CategoryService.UpdateAsync(viewModel);
            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public Task<Result> DeleteByIdAsync(Guid id)
        {
            var result = CategoryService.DeleteByIdAsync(id);
            return result;
        }

        [HttpGet("GetByIdAsync")]
        Task<Result<CategoryViewModel>> GetByIdAsync(Guid id)
        {
            var result = CategoryService.GetByIdAsync(id);
            return result;
        }

        [HttpGet("GetAllAsync")]
        public Task<Result<ICollection<CategoryViewModel>>> GetAllAsync()
        {
            var result = CategoryService.GetAllAsync();
            return result;
        }

        [HttpGet("GetByOwnerIdAsync")]
        public Task<Result<ICollection<CategoryViewModel>>> GetByOwnerIdAsync(Guid ownerId)
        {
            var result = CategoryService.GetAllAsync();
            return result;
        }

    }
}

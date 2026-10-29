using FactorMaker.Infrastructure;
using FactorMaker.Infrastructure.Attributes;
using Common;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Category;
using ViewModels.PostCategory;

namespace FactorMaker.Controllers
{
    [Authenticated]
    public class PostCategoryController : BaseApiControllerWithUser
    {
        private PostCategoryController() : base()
        {

        }
        public PostCategoryController(IPostCategoryService postCategoryService, AccessGuard accessGuard)
        {
            PostCategoryService = postCategoryService;
            AccessGuard = accessGuard;
        }
        private IPostCategoryService PostCategoryService { get; }
        private AccessGuard AccessGuard { get; }


        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(PostCategoryViewModel viewModel)
        {
            viewModel.OwnerId = User.Id;
            var result = await PostCategoryService.InsertAsync(viewModel);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(PostCategoryViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessPostCategoryAsync(User, viewModel.Id)) return Result(AccessGuard.Denied());
            if (!AccessGuard.IsAdmin(User)) viewModel.OwnerId = User.Id;
            var result = await PostCategoryService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessPostCategoryAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await PostCategoryService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessPostCategoryAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await PostCategoryService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await PostCategoryService.GetAllAsync();
            AccessGuard.FilterByOwner(User, result, x => x.OwnerId);
            return Result(result);
        }

        [HttpGet("GetByOwnerIdAsync")]
        public async Task<IActionResult> GetByOwnerAsync()
        {
            var result = await PostCategoryService.GetByOwnerIdAsync(User.Id);
            return Result(result);
        }
    }
}

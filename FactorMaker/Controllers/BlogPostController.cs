using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.BlgoPost;

namespace FactorMaker.Controllers
{
    //[Authorize]
    public class BlogPostController : BaseApiControllerWithUser
    {
        private BlogPostController() : base()
        {

        }
        public BlogPostController(IBlogPostService blogPostService)
        {
            BlogPostService = blogPostService;
        }
        
        private IBlogPostService BlogPostService { get; }

        [Authorize]
        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(BlogPostViewModel viewModel)
        {
            var result = await BlogPostService.InsertAsync(viewModel);
            viewModel.OwnerId = User.Id;
            return Result(result);
        }
        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(BlogPostViewModel viewModel)
        {
            var result = await BlogPostService.UpdateAsync(viewModel);
            return Result(result);
        }
        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await BlogPostService.DeleteByIdAsync(id);
            return Result(result);
        }
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await BlogPostService.GetByIdAsync(id);
            return Result(result);
        }
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await BlogPostService.GetAllAsync();
            return Result(result);
        }
        [HttpGet("GetPublishedAsync")]
        public async Task<IActionResult> GetPublishedAsync()
        {
            var result = await BlogPostService.GetPublishedAsync();
            return Result(result);
        }
        [HttpGet("GetPublishedHotAsync")]
        public async Task<IActionResult> GetPublishedHotAsync()
        {
            var result = await BlogPostService.GetPublishedHotAsync();
            return Result(result);
        }
        [HttpGet("GetsNotPublishedAsync")]
        public async Task<IActionResult> GetsNotPublishedAsync()
        {
            var result = await BlogPostService.GetsNotPublishedAsync();
            return Result(result);
        }
        [HttpGet("GetByPostCategoryIdAsync")]
        public async Task<IActionResult> GetByPostCategoryIdAsync(Guid postCategoryId)
        {
            var result = await BlogPostService.GetByPostCategoryIdAsync(postCategoryId);
            return Result(result);
        }
        [HttpGet("GetByOwnerIdAsync")]
        public async Task<IActionResult> GetByOwnerIdAsync(Guid ownerId)
        {
            var result = await BlogPostService.GetByOwnerIdAsync(ownerId);
            return Result(result);
        }
        [HttpGet("GetByOwnerPublishedAsync")]
        public async Task<IActionResult> GetByOwnerPublishedAsync(Guid ownerId)
        {
            var result = await BlogPostService.GetByOwnerPublishedAsync(ownerId);
            return Result(result);
        }
        [HttpGet("GetByOwnerPublishedHotAsync")]
        public async Task<IActionResult> GetByOwnerPublishedHotAsync(Guid ownerId)
        {
            var result = await BlogPostService.GetByOwnerPublishedHotAsync(ownerId);
            return Result(result);
        }
        [HttpGet("GetByOwnerNotPublishedAsync")]
        public async Task<IActionResult> GetByOwnerNotPublishedAsync(Guid ownerId)
        {
            var result = await BlogPostService.GetByOwnerNotPublishedAsync(ownerId);
            return Result(result);
        }
    }
}

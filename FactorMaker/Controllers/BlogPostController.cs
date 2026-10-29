using FactorMaker.Infrastructure;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.BlgoPost;

namespace FactorMaker.Controllers
{
    [Authenticated]
    public class BlogPostController : BaseApiControllerWithUser
    {
        private BlogPostController() : base()
        {

        }
        public BlogPostController(IBlogPostService blogPostService, AccessGuard accessGuard)
        {
            BlogPostService = blogPostService;
            AccessGuard = accessGuard;
        }
        
        private IBlogPostService BlogPostService { get; }
        
        private AccessGuard AccessGuard { get; }

        [Authorize]
        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(BlogPostViewModel viewModel)
        {
            if (User != null)
            {
                viewModel.OwnerId = User.Id;
            }

            var result = await BlogPostService.InsertAsync(viewModel);
            return Result(result);
        }
        
        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(BlogPostViewModel viewModel)
        {
            if (!await AccessGuard.CanAccessBlogPostAsync(User, viewModel.Id)) return Result(AccessGuard.Denied());
            if (!AccessGuard.IsAdmin(User)) viewModel.OwnerId = User.Id;
            var result = await BlogPostService.UpdateAsync(viewModel);
            return Result(result);
        }
        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            if (!await AccessGuard.CanAccessBlogPostAsync(User, id)) return Result(AccessGuard.Denied());
            var result = await BlogPostService.DeleteByIdAsync(id);
            return Result(result);
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
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
            AccessGuard.FilterByOwner(User, result, x => x.OwnerId);
            return Result(result);
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("GetPublishedAsync")]
        public async Task<IActionResult> GetPublishedAsync()
        {
            var result = await BlogPostService.GetPublishedAsync();
            return Result(result);
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("GetPublishedHotAsync")]
        public async Task<IActionResult> GetPublishedHotAsync()
        {
            var result = await BlogPostService.GetPublishedHotAsync();
            return Result(result);
        }
        [HttpGet("GetNotPublishedAsync")]
        public async Task<IActionResult> GetNotPublishedAsync()
        {
            var result = await BlogPostService.GetNotPublishedAsync();
            AccessGuard.FilterByOwner(User, result, x => x.OwnerId);
            return Result(result);
        }
        [HttpGet("GetByPostCategoryIdAsync")]
        public async Task<IActionResult> GetByPostCategoryIdAsync(Guid postCategoryId)
        {
            var result = await BlogPostService.GetByPostCategoryIdAsync(postCategoryId);
            AccessGuard.FilterByOwner(User, result, x => x.OwnerId);
            return Result(result);
        }
        [HttpGet("GetByOwnerIdAsync")]
        public async Task<IActionResult> GetByOwnerIdAsync(Guid ownerId)
        {
            if (!AccessGuard.IsAdmin(User) && ownerId != User.Id) return Result(AccessGuard.Denied());
            var result = await BlogPostService.GetByOwnerIdAsync(ownerId);
            return Result(result);
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("GetByOwnerPublishedAsync")]
        public async Task<IActionResult> GetByOwnerPublishedAsync(Guid ownerId)
        {
            var result = await BlogPostService.GetByOwnerPublishedAsync(ownerId);
            return Result(result);
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("GetByOwnerPublishedHotAsync")]
        public async Task<IActionResult> GetByOwnerPublishedHotAsync(Guid ownerId)
        {
            var result = await BlogPostService.GetByOwnerPublishedHotAsync(ownerId);
            return Result(result);
        }
        [HttpGet("GetByOwnerNotPublishedAsync")]
        public async Task<IActionResult> GetByOwnerNotPublishedAsync(Guid ownerId)
        {
            if (!AccessGuard.IsAdmin(User) && ownerId != User.Id) return Result(AccessGuard.Denied());
            var result = await BlogPostService.GetByOwnerNotPublishedAsync(ownerId);
            return Result(result);
        }
    }
}

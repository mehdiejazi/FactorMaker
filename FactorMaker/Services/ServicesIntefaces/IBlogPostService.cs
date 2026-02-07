using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.BlgoPost;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface IBlogPostService
    {
        Task<Result<BlogPostViewModel>> InsertAsync(BlogPostViewModel viewModel);
        Task<Result<BlogPostViewModel>> UpdateAsync(BlogPostViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<BlogPostViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<BlogPostViewModel>>> GetAllAsync();
        Task<Result<ICollection<BlogPostViewModel>>> GetPublishedAsync();
        Task<Result<ICollection<BlogPostViewModel>>> GetPublishedHotAsync();
        Task<Result<ICollection<BlogPostViewModel>>> GetNotPublishedAsync();
        Task<Result<ICollection<BlogPostViewModel>>> GetByPostCategoryIdAsync(Guid postCategoryId);
        Task<Result<ICollection<BlogPostViewModel>>> GetByOwnerIdAsync(Guid ownerId);
        Task<Result<ICollection<BlogPostViewModel>>> GetByOwnerPublishedAsync(Guid ownerId);
        Task<Result<ICollection<BlogPostViewModel>>> GetByOwnerPublishedHotAsync(Guid ownerId);
        Task<Result<ICollection<BlogPostViewModel>>> GetByOwnerNotPublishedAsync(Guid ownerId);
    }
}
using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.PostCategory;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IPostCategoryService
    {
        Task<Result<PostCategoryViewModel>> InsertAsync(PostCategoryViewModel viewModel);
        Task<Result<PostCategoryViewModel>> UpdateAsync(PostCategoryViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<PostCategoryViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<PostCategoryViewModel>>> GetAllAsync();
        Task<Result<ICollection<PostCategoryViewModel>>> GetByOwnerIdAsync(Guid ownerId);
    }
}
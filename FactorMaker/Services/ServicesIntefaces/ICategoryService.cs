using Common;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface ICategoryService
    {
        Task<Result<CategoryViewModel>> InsertAsync(CategoryViewModel viewModel);
        Task<Result<CategoryViewModel>> UpdateAsync(CategoryViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<CategoryViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<CategoryViewModel>>> GetAllAsync();
        Task<Result<ICollection<CategoryViewModel>>> GetByOwnerIdAsync(Guid ownerId);
    }
}
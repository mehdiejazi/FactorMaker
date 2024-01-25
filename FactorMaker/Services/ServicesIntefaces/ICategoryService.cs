using Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ViewModels.Category;
using Models;

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
        Task<Result<ICollection<CategorySaleTotalPriceViewModel>>> GetSaleTotalByPriceAsync
            (User user,DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<Result<ICollection<CategorySaleTotalQuantityViewModel>>> GetSaleTotalQuantityAsync
            (User user,DateTime dtFrom, DateTime dtTo, Guid storeId);
    }
}
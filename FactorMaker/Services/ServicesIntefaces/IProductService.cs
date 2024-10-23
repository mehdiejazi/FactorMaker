using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Product;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface IProductService
    {
        Task<Result<ProductViewModel>> InsertAsync(ProductViewModel viewModel);
        Task<Result<ProductViewModel>> UpdateAsync(ProductViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<ProductViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<ProductViewModel>>> GetAllAsync();
        Task<Result<ICollection<ProductViewModel>>> GetByStoreIdAsync(Guid ownerId);
        Task<Result<ICollection<ProductViewModel>>> GetByStoreIdCategoryIdAsync(Guid ownerId, Guid categoryId);
        Task<Result<ICollection<ProductSaleTotalQuantityViewModel>>> GetTop10SaleByQuantityAsync(User user, Guid storeId);
        Task<Result<ICollection<ProductSaleTotalPriceViewModel>>> GetTop10SaleByPriceAsync(User user, Guid storeId);
        Task<Result<ICollection<ProductSaleTotalQuantityViewModel>>> GetSaleTotalByQuantityAsync
            (User user, DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<Result<ICollection<ProductSaleTotalPriceViewModel>>> GetSaleTotalByPriceAsync
            (User user,DateTime dtFrom, DateTime dtTo, Guid storeId);

    }
}
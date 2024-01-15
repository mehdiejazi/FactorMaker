using Common;
using Data.DataTransferObjects.Product;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Product;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IProductService
    {
        Task<Result<ProductViewModel>> InsertAsync(ProductViewModel viewModel);
        Task<Result<ProductViewModel>> UpdateAsync(ProductViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<ProductViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<ProductViewModel>>> GetAllAsync();
        Task<Result<ICollection<ProductViewModel>>> GetByOwnerIdCategoryIdAsync(Guid ownerId, Guid categoryId);
        Task<Result<ICollection<ProductSaleTotalQuantityViewModel>>> GetTop10SaleByQuantityAsync(Guid storeId);
        Task<Result<ICollection<ProductSaleTotalPriceViewModel>>> GetTop10SaleByPriceAsync(Guid storeId);
        Task<Result<ICollection<ProductSaleTotalQuantityViewModel>>> GetSaleTotalByQuantityAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<Result<ICollection<ProductSaleTotalPriceViewModel>>> GetSaleTotalByPriceAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId);

    }
}
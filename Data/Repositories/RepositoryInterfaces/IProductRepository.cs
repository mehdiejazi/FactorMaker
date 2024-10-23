using Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Data.DataTransferObjects.Product;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IProductRepository : Data.Base.IRepository<Product>
    {
        Task<ICollection<Product>> GetByStoreIdAsync(Guid storeId);
        Task<ICollection<Product>> GetByStoreIdCategoryIdAsync(Guid storeId, Guid categoryId);
        Task<ICollection<ProductSaleTotalQuantityDto>> GetTop10SaleByQuantityAsync(Guid storeId);
        Task<ICollection<ProductSaleTotalPriceDto>> GetTop10SaleByPriceAsync(Guid storeId);
        Task<ICollection<ProductSaleTotalQuantityDto>> GetSaleTotalByQuantityAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<ICollection<ProductSaleTotalPriceDto>> GetSaleTotalByPriceAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId);
    }
}

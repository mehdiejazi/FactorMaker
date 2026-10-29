using Data.DataTransferObjects.Product;
using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        internal ProductRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<Product>> GetByStoreIdAsync(Guid storeId)
        {
            var list = await DbSet
                .Where(u => u.StoreId == storeId && u.IsDeleted == false)
                .Include(p => p.Category)
                .Include(p => p.Image)
                .OrderBy(x => x.SortOrder).ThenBy(x => x.InsertDateTime)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<Product>> GetByStoreIdCategoryIdAsync(Guid storeId, Guid categoryId)
        {
            var list = await DbSet
                .Where(u => u.StoreId.Equals(storeId) && u.CategoryId.Equals(categoryId) && u.IsDeleted == false)
                .Include(p => p.Category)
                .Include(p => p.Image)
                .OrderBy(x => x.SortOrder).ThenBy(x => x.InsertDateTime)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<ProductSaleTotalQuantityDto>> GetTop10SaleByQuantityAsync(Guid storeId)
        {
            var list = (await DatabaseContext.Set<FactorItem>()
                .Where(fi => fi.IsDeleted == false && fi.Factor.StoreId == storeId && fi.Factor.IsDeleted == false && fi.Factor.IsClosed)
                .Include(x => x.Product)
                .ToListAsync())
                .GroupBy(fi => fi.Product.Id)
                .OrderByDescending(g => g.Sum(fi => fi.Quantity))
                .Take(10)
                .Select(g => new ProductSaleTotalQuantityDto
                {
                    Product = g.First().Product,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .ToList();

            return list;
        }

        public async Task<ICollection<ProductSaleTotalPriceDto>> GetTop10SaleByPriceAsync(Guid storeId)
        {
            var list = (await DatabaseContext.Set<FactorItem>()
                .Where(fi => fi.IsDeleted == false && fi.Factor.StoreId == storeId && fi.Factor.IsDeleted == false && fi.Factor.IsClosed)
                .Include(x => x.Product)
                .ToListAsync())
                .GroupBy(fi => fi.Product.Id)
                .OrderByDescending(g => g.Sum(fi => fi.Price * fi.Quantity * (1 - (fi.OffPercent / 100m))))
                .Take(10)
                .Select(g => new ProductSaleTotalPriceDto
                {
                    Product = g.First().Product,
                    TotalPrice = g.Sum(x => x.Price * x.Quantity * (1 - (x.OffPercent / 100m)))
                })
                .ToList();

            return list;
        }

        public async Task<ICollection<ProductSaleTotalQuantityDto>> GetSaleTotalByQuantityAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var list = (await DatabaseContext.Set<FactorItem>()
                .Where(fi => fi.IsDeleted == false && fi.Factor.StoreId == storeId && fi.Factor.IsDeleted == false && fi.Factor.IsClosed && fi.Factor.SellDateTime >= dtFrom && fi.Factor.SellDateTime < dtTo)
                .Include(x => x.Product)
                .ToListAsync())
                .GroupBy(fi => fi.Product.Id)
                .OrderByDescending(g => g.Sum(fi => fi.Quantity))
                .Select(g => new ProductSaleTotalQuantityDto
                {
                    Product = g.First().Product,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .ToList();

            return list;
        }

        public async Task<ICollection<ProductSaleTotalPriceDto>> GetSaleTotalByPriceAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var list = (await DatabaseContext.Set<FactorItem>()
               .Where(fi => fi.IsDeleted == false && fi.Factor.StoreId == storeId && fi.Factor.IsDeleted == false && fi.Factor.IsClosed && fi.Factor.SellDateTime >= dtFrom && fi.Factor.SellDateTime < dtTo)
               .Include(x => x.Product)
                .ToListAsync())
                .GroupBy(fi => fi.Product.Id)
               .OrderByDescending(g => g.Sum(fi => fi.Price * fi.Quantity * (1 - (fi.OffPercent / 100m))))
               .Select(g => new ProductSaleTotalPriceDto
               {
                   Product = g.First().Product,
                   TotalPrice = g.Sum(x => x.Price * x.Quantity * (1 - (x.OffPercent / 100m)))
               })
               .ToList();

            return list;
        }
    }
}

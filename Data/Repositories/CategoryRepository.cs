using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Data.DataTransferObjects.Category;

namespace Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        internal CategoryRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<Category>> GetByStoreIdAsync(Guid storeId)
        {
            var list = await DbSet
                .Where(u => u.StoreId.Equals(storeId) && u.IsDeleted == false)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<CategorySaleTotalPriceDto>> GetSaleTotalByPriceAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var list = (await DatabaseContext.Set<FactorItem>()
                .Where(x => x.IsDeleted == false && x.Factor.IsDeleted == false && x.Factor.IsClosed && x.Factor.SellDateTime >= dtFrom && x.Factor.SellDateTime < dtTo && x.Factor.StoreId.Equals(storeId))
                .Include(x => x.Product).ThenInclude(x => x.Category)
                .ToListAsync())
                .GroupBy(f => f.Product.Category.Id)
                .Select(group => new CategorySaleTotalPriceDto
                {
                    Category = group.First().Product.Category,
                    TotalPrice = group.Sum(x => x.Price * x.Quantity * (1 - (x.OffPercent / 100m)))
                })
                .OrderByDescending(x => x.TotalPrice)
                .ToList();

            return list;
        }

        public async Task<ICollection<CategorySaleTotalQuantityDto>> GetSaleTotalByQuantityAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var list = (await DatabaseContext.Set<FactorItem>()
                .Where(x => x.IsDeleted == false && x.Factor.IsDeleted == false && x.Factor.IsClosed && x.Factor.SellDateTime >= dtFrom && x.Factor.SellDateTime < dtTo && x.Factor.StoreId.Equals(storeId))
                .Include(x => x.Product).ThenInclude(x => x.Category)
                .ToListAsync())
                .GroupBy(f => f.Product.Category.Id)
                .Select(group => new CategorySaleTotalQuantityDto
                {
                    Category = group.First().Product.Category,
                    TotalQuantity = group.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .ToList();

            return list;
        }
    }
}

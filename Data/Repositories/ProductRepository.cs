﻿using Data.DataTransferObjects.Product;
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
                .Where(u => 
                u.StoreId == storeId && 
                u.IsDeleted == false)
                .Include(p=>p.Category)
                .OrderBy(x => x.InsertDateTime)
                .ToListAsync();

            return list;
        }
        public async Task<ICollection<Product>> GetByStoreIdCategoryIdAsync(Guid storeId, Guid categoryId)
        {
            var list = await DbSet
                .Where(u => 
                u.StoreId.Equals(storeId) && 
                u.Category.Id.Equals(categoryId) && 
                u.IsDeleted == false)
                .Include(p => p.Category)
                .OrderBy(x => x.InsertDateTime)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<ProductSaleTotalQuantityDto>> GetTop10SaleByQuantityAsync(Guid storeId)
        {
            var list = await DatabaseContext.Set<FactorItem>()
                .Where(fi =>
                    fi.IsDeleted == false &&
                    fi.Factor.StoreId == storeId &&
                    fi.Factor.IsDeleted == false &&
                    fi.Factor.IsClosed)
                .GroupBy(fi => fi.Product)
                .OrderByDescending(g => g.Sum(fi => fi.Quantity))
                .Take(10)
                .Select(g => new ProductSaleTotalQuantityDto
                {
                    Product = g.Key,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<ProductSaleTotalPriceDto>> GetTop10SaleByPriceAsync(Guid storeId)
        {
            var list = await DatabaseContext.Set<FactorItem>()
                .Where(fi =>
                    fi.IsDeleted == false &&
                    fi.Factor.StoreId == storeId &&
                    fi.Factor.IsDeleted == false &&
                    fi.Factor.IsClosed)
                .GroupBy(fi => fi.Product)
                .OrderByDescending(g => g.Sum(fi => fi.Price))
                .Take(10)
                .Select(g => new ProductSaleTotalPriceDto
                {
                    Product = g.Key,
                    TotalPrice = g.Sum(x => x.Price)
                })
                .ToListAsync();

            return list;

        }

        public async Task<ICollection<ProductSaleTotalQuantityDto>> GetSaleTotalByQuantityAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var list = await DatabaseContext.Set<FactorItem>()
                .Where(fi =>
                    fi.IsDeleted == false &&
                    fi.Factor.StoreId == storeId &&
                    fi.Factor.IsDeleted == false &&
                    fi.Factor.IsClosed &&
                    fi.Factor.SellDateTime >= dtFrom &&
                    fi.Factor.SellDateTime < dtTo)
                .GroupBy(fi => fi.Product)
                .OrderByDescending(g => g.Sum(fi => fi.Quantity))
                .Select(g => new ProductSaleTotalQuantityDto
                {
                    Product = g.Key,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<ProductSaleTotalPriceDto>> GetSaleTotalByPriceAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var list = await DatabaseContext.Set<FactorItem>()
               .Where(fi =>
                    fi.IsDeleted == false &&
                    fi.Factor.StoreId == storeId &&
                    fi.Factor.IsDeleted == false &&
                    fi.Factor.IsClosed &&
                    fi.Factor.SellDateTime >= dtFrom &&
                    fi.Factor.SellDateTime < dtTo)
               .GroupBy(fi => fi.Product)
               .OrderByDescending(g => g.Sum(fi => fi.Price))
               .Select(g => new ProductSaleTotalPriceDto
               {
                   Product = g.Key,
                   TotalPrice = g.Sum(x => x.Price)
               })
               .ToListAsync();

            return list;
        }
    }
}

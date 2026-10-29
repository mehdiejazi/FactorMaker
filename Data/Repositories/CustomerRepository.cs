using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Data.DataTransferObjects.Customer;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        internal CustomerRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

        public async Task<ICollection<Customer>> GetByStoreIdAsync(Guid storeId)
        {
            var list = await DbSet
                .Where(s => s.StoreId.Equals(storeId) && s.IsDeleted == false)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<CustomerSaleTotalQuantityDto>> GetTop10CustomersByQuantityAsync(Guid storeId)
        {
            var list = (await DatabaseContext.Set<FactorItem>()
                .Where(fi => fi.IsDeleted == false
                    && fi.Factor.StoreId == storeId
                    && fi.Factor.IsDeleted == false
                    && fi.Factor.IsClosed)
                .Include(x => x.Factor).ThenInclude(x => x.Owner)
                .ToListAsync())
                .GroupBy(fi => fi.Factor.Owner.Id)
                .OrderByDescending(g => g.Sum(fi => fi.Quantity))
                .Take(10)
                .Select(g => new CustomerSaleTotalQuantityDto
                {
                    Customer = g.First().Factor.Owner,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .ToList();

            return list;
        }

        public async Task<ICollection<CustomerSaleTotalPriceDto>> GetTop10CustomersByPriceAsync(Guid storeId)
        {
            var list = (await DatabaseContext.Set<FactorItem>()
                .Where(fi => fi.IsDeleted == false
                    && fi.Factor.StoreId == storeId
                    && fi.Factor.IsDeleted == false
                    && fi.Factor.IsClosed)
                .Include(x => x.Factor).ThenInclude(x => x.Owner)
                .ToListAsync())
                .GroupBy(fi => fi.Factor.Owner.Id)
                .OrderByDescending(g => g.Sum(fi => fi.Price * fi.Quantity * (1 - (fi.OffPercent / 100m))))
                .Take(10)
                .Select(g => new CustomerSaleTotalPriceDto
                {
                    Customer = g.First().Factor.Owner,
                    TotalPrice = g.Sum(x => x.Price * x.Quantity * (1 - (x.OffPercent / 100m)))
                })
                .ToList();

            return list;
        }

        public async Task<ICollection<CustomerGenderStatDto>> GetGenderStatsByStoreIdAsync(Guid storeId)
        {
            var stats = (await DbSet
                .Where(customer => customer.StoreId == storeId && customer.IsDeleted == false)
                .ToListAsync())
                .GroupBy(customer => customer.Gender)
                .Select(group => new CustomerGenderStatDto
                {
                    Gender = group.Key,
                    Count = group.Count()
                })
                .ToList();

            return stats;
        }
    }
}

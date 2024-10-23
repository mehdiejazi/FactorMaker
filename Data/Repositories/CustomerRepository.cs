using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
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

        public async Task<ICollection<Customer>> GetTop10CustomersByQuantityAsync(Guid storeId)
        {
            var list = await DatabaseContext.Set<Factor>()
                .Where(f => f.StoreId == storeId)
                .SelectMany(f => f.FactorItems)
                .GroupBy(fi => fi.Factor.Owner)
                .OrderByDescending(g => g.Sum(fi => fi.Quantity))
                .Take(10)
                .Select(g => g.Key)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<Customer>> GetTop10CustomersByPriceAsync(Guid storeId)
        {
            var list = await DatabaseContext.Set<Factor>()
                .Where(f => f.StoreId == storeId)
                .SelectMany(f => f.FactorItems)
                .GroupBy(fi => fi.Factor.Owner)
                .OrderByDescending(g => g.Sum(fi => fi.Price))
                .Take(10)
                .Select(g => g.Key)
                .ToListAsync();

            return list;
        }


    }
}

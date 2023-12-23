using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        internal ProductRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<Product>> GetByOwnerIdCategoryIdAsync(Guid ownerId,Guid categoryId)
        {
            var list = await DbSet
                .Where(u => u.Owner.Id.Equals(ownerId) && u.Category.Id.Equals(categoryId))
                .ToListAsync();

            return list;
        }
    }
}

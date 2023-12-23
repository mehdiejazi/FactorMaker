using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        internal CategoryRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

        public async Task<ICollection<Category>> GetByOwnerIdAsync(Guid ownerId)
        {
            var list = await DbSet
                .Where(u => u.Owner.Id.Equals(ownerId))
                .ToListAsync();
                
            return list;
        }
    }
}

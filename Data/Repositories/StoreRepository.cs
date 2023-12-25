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
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        internal StoreRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<Store>> GetByOwnerIdAsync(Guid ownerId)
        {
            var list = await DbSet.Where(x => x.OwnerId.Equals(ownerId)).ToListAsync();
            return list;
        }

        public async Task<Store> GetByStoreIdAsync(Guid storeId)
        {
            var entity = await DbSet.Where(x => x.StoreId.Equals(storeId)).FirstOrDefaultAsync();
            return entity;
        }
    }
}

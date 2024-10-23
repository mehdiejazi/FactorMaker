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
        public async new Task<Store> GetByIdAsync(Guid Id)
        {
            var user = await DbSet
                .Where(u => u.Id.Equals(Id))
                .Include(u => u.Logo)
                .Include(u=>u.Owner)
                .FirstOrDefaultAsync();

            return user;
        }
        public async Task<ICollection<Store>> GetByOwnerIdAsync(Guid ownerId)
        {
            var list = await DbSet
                .Where(x => 
                x.OwnerId.Equals(ownerId) &&
                x.IsDeleted == false)
                .Include(u => u.Logo)
                .Include(u => u.Owner)
                .ToListAsync();
            return list;
        }

        public async Task<Store> GetByStoreEnglishNameAsync(string storeEnglishName)
        {
            var entity = await DbSet
                .Where(x => 
                x.StoreEnglishName.Equals(storeEnglishName) &&
                x.IsDeleted == false)
                .Include(u => u.Logo)
                .Include(u => u.Owner)
                .FirstOrDefaultAsync();
            return entity;
        }

        public async Task<bool> IsExistByStoreEnglishNameAsync(string storeEnglishName)
        {
            var ret = await DbSet
                    .AnyAsync(x => x.StoreEnglishName == storeEnglishName);
            return ret;
        }
        public new async Task<ICollection<Store>> GetAllAsync()
        {
            var list = await DbSet
                .Include(u => u.Logo)
                .Include(u => u.Owner)
                .OrderBy(u => u.InsertDateTime)
                .ToListAsync();

            return list;
        }
    }
}

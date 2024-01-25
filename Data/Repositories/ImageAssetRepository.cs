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
    public class ImageAssetRepository : RepositoryBase<ImageAsset>, IImageAssetRepository
    {
        internal ImageAssetRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public new async Task<ICollection<ImageAsset>> GetAllAsync()
        {
            var list = await DbSet
                .OrderByDescending(u => u.InsertDateTime)
                .ToListAsync();

            return list;
        }


        public async new Task<ImageAsset> GetByIdWithRefreshTokenAsync(Guid Id)
        {
            var item = await DbSet
                .Where(u => u.Id.Equals(Id))
                .Include(O=>O.Owner)
                .FirstOrDefaultAsync();

            return item;
        }

        public async Task<ICollection<ImageAsset>> GetByUserAsync(User user)
        {
            var result = await
                DbSet
                .Where(current => current.Owner.Id.Equals(user.Id))
                .Include(x => x.Owner)
                .OrderByDescending(x => x.InsertDateTime)
                .ToListAsync();

            return result;
        }


        public async Task<ICollection<ImageAsset>> GetNotDeletedByUserAsync(User user)
        {
            var result = await
                DbSet
                .Where(current => current.Owner.Id.Equals(user.Id)
                    && current.IsDeleted == false)
                .Include(x=>x.Owner)
                .OrderByDescending(x => x.InsertDateTime)
                .ToListAsync();

            return result;
        }

        public async Task<ICollection<ImageAsset>> GetDeletedByUserAsync(User user)
        {
            var result = await
                DbSet
                .Where(current => current.Owner.Id.Equals(user.Id)
                    && current.IsDeleted == true)
                .Include(x => x.Owner)
                .OrderByDescending(x => x.InsertDateTime)
                .ToListAsync();

            return result;
        }

        public async Task<ICollection<ImageAsset>> GetByRoleAsync(Role role)
        {
            var result = await
                DbSet
                .Where(current => current.Owner.Role == role)
                .Include(x => x.Owner)
                .OrderByDescending(x => x.InsertDateTime)
                .ToListAsync();

            return result;
        }


        public async Task<ICollection<ImageAsset>> GetNotDeletedByRoleAsync(Role role)
        {
            var result = await
                DbSet
                .Where(current => current.Owner.Role == role
                    && current.IsDeleted == false)
                .Include(x => x.Owner)
                .OrderByDescending(x => x.InsertDateTime)
                .ToListAsync();

            return result;
        }

        public async Task<ICollection<ImageAsset>> GetDeletedByRoleAsync(Role role)
        {
            var result = await
                DbSet
                .Where(current => current.Owner.Role == role
                    && current.IsDeleted == true)
                .Include(x => x.Owner)
                .OrderByDescending(x => x.InsertDateTime)
                .ToListAsync();

            return result;
        }

    }
}

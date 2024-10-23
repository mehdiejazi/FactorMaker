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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        internal UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }


        public async new Task<User> GetByIdAsync(Guid Id)
        {
            var user = await DbSet
                .Where(u => u.Id.Equals(Id))
                .Include(u => u.Role)
                .ThenInclude(p => p.RoleActionPermissions)
                .ThenInclude(a => a.ActionPermission)
                .Include(u => u.Avatar)
                .Include(u => u.RefreshToken)
                .Include(u => u.Stores)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await DbSet
                .Where(u => u.UserName.ToLower() == userName.ToLower())
                .Include(u => u.Role)
                .ThenInclude(p => p.RoleActionPermissions)
                .ThenInclude(a => a.ActionPermission)
                .Include(u => u.Avatar)
                .Include(u => u.RefreshToken)
                .Include(u => u.Stores)

                .FirstOrDefaultAsync();
            return user;
        }

        public new async Task<ICollection<User>> GetAllAsync()
        {
            var list = await DbSet
                .Include(u => u.Avatar)
                .Include(u => u.Role)
                .Include(u => u.Stores)
                .OrderBy(u => u.InsertDateTime)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<User>> GetActiveAsync()
        {
            var list = await DbSet
                .Where(u => u.IsActive)
                .Include(u => u.Avatar)
                .Include(u => u.Role)
                .Include(u=>u.Stores)
                .OrderBy(u => u.InsertDateTime)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<User>> GetInActiveAsync()
        {
            var list = await DbSet
                .Where(u => u.IsActive == false)
                .Include(u => u.Avatar)
                .Include(u => u.Role)
                .Include(u => u.Stores)
                .OrderBy(u => u.InsertDateTime)
                .ToListAsync();

            return list;
        }

        public async Task<bool> IsExistByUsernameAsync(string userName)
        {
            var ret = await DbSet
                .AnyAsync(u => u.UserName == userName);

            return ret;
        }

        public async Task<bool> HasAccessToStoreAsync(Guid id, Guid storeId)
        {
            bool hasAny = await DbSet
                .AnyAsync(u => u.Id.Equals(id) && u.Stores.Any(s => s.Id.Equals(storeId)));

            return hasAny;

        }

        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            var user = await DbSet
                .Where(u => u.RefreshToken.RefreshToken == refreshToken)
                .Include(u => u.RefreshToken)
                .FirstOrDefaultAsync();

            return user;
        }


    }
}

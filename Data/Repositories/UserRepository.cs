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


        public new ICollection<User> GetAll()
        {
            var list = DbSet
                .OrderBy(u => u.InsertDateTime)
                .ToList();

            return list;
        }
        public new async Task<ICollection<User>> GetAllAsync()
        {
            var list = await DbSet
                .OrderBy(u => u.InsertDateTime)
                .ToListAsync();

            return list;
        }

        public new User GetById(Guid Id)
        {
            var user = DbSet
                .Where(u => u.Id.Equals(Id))
                .Include(u => u.Role)
                .ThenInclude(p=>p.RoleActionPermissions)
                .ThenInclude(a => a.ActionPermission)
                .FirstOrDefault();

            return user;
        }

        public async new Task<User> GetByIdAsync(Guid Id)
        {
            var user = await DbSet
                .Where(u => u.Id.Equals(Id))
                .Include(u => u.Role)
                .ThenInclude(p => p.RoleActionPermissions)
                .ThenInclude(a => a.ActionPermission)
                .FirstOrDefaultAsync();

            return user;
        }

        public User GetByUserName(string userName)
        {
            var user = DbSet.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
            return user;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await DbSet.Where(u => u.UserName.ToLower() == userName.ToLower())
                .FirstOrDefaultAsync();
            return user;
        }

        public ICollection<User> GetActive()
        {
            var list = DbSet
               .Where(u => u.IsActive)
               .OrderBy(u => u.InsertDateTime)
               .ToList();

            return list;
        }

        public async Task<ICollection<User>> GetActiveAsync()
        {
            var list = await DbSet
                .Where(u => u.IsActive)
                .OrderBy(u => u.InsertDateTime)
                .ToListAsync();

            return list;
        }

        public ICollection<User> GetInActive()
        {
            var list = DbSet
                .Where(u => u.IsActive == false)
                .OrderBy(u => u.InsertDateTime)
                .ToList();

            return list;
        }

        public async Task<ICollection<User>> GetInActiveAsync()
        {
            var list = await DbSet
                .Where(u => u.IsActive == false)
                .OrderBy(u => u.InsertDateTime)
                .ToListAsync();

            return list;
        }
    }
}

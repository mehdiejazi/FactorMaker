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
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        internal RoleRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public new ICollection<Role> GetAll()
        {
            var list = DbSet
                .OrderBy(u => u.InsertDateTime)
                .ToList();

            return list;
        }
        public new async Task<ICollection<Role>> GetAllAsync()
        {
            var list = await DbSet
                .OrderBy(u => u.InsertDateTime)
                .ToListAsync();

            return list;
        }

        public new Role GetById(Guid Id)
        {
            var role = DbSet
                .Where(u => u.Id.Equals(Id))
                .Include(u => u.RoleActionPermissions)
                .ThenInclude(rap => rap.ActionPermission)
                .FirstOrDefault();

            return role;
        }

        public async new Task<Role> GetByIdAsync(Guid Id)
        {
            var role = await DbSet
                .Where(u => u.Id.Equals(Id))
                .Include(u => u.RoleActionPermissions)
                .ThenInclude(rap => rap.ActionPermission)
                .FirstOrDefaultAsync();

            return role;
        }

    }
}

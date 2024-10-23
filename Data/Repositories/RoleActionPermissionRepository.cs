using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RoleActionPermissionRepository : RepositoryBase<RoleActionPermission>, IRoleActionPermissionRepository
    {
        internal RoleActionPermissionRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

        public async Task<bool> AnyByRoleIdActionPermissionIdAsync(Guid roleId, Guid actionPermissionId)
        {
            var result = await DbSet.AnyAsync(current => current.RoleId == roleId &&
                    current.ActionPermissionId == actionPermissionId);

            return result;
        }
    }
}

using Models;
using System;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IRoleActionPermissionRepository : Data.Base.IRepository<RoleActionPermission>
    {
        Task<bool> AnyByRoleIdActionPermissionIdAsync(Guid roleId, Guid actionPermissionId);
    }

    
}

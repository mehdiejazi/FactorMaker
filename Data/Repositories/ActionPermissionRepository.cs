using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Models;

namespace Data.Repositories
{
    public class ActionPermissionRepository : RepositoryBase<ActionPermission>, IActionPermissionRepository
    {
        internal ActionPermissionRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

      
    }
}

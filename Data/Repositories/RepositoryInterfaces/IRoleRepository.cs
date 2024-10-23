using Models;
using System;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IRoleRepository : Data.Base.IRepository<Role>
    {
        Task<Role> GetDefaultRoleAsync();
    }
}

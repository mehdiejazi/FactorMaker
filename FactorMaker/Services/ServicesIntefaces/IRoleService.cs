using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IRoleService
    {
        void DeleteById(Guid id);
        Task DeleteByIdAsync(Guid id);
        ICollection<Role> GetAll();
        Task<ICollection<Role>> GetAllAsync();
        Role GetById(Guid id);
        Task<Role> GetByIdAsync(Guid id);
        Role Insert(string roleName, string description);
        Task<Role> InsertAsync(string roleName, string description);
        Role Update(Guid id, string roleName, string description);
        Task<Role> UpdateAsync(Guid id, string roleName, string description);
        Role AddActionPermission(Guid roleId, Guid actionPermissionId);
        Task<Role> AddActionPermissionAsync(Guid roleId, Guid actionPermissionId);

    }
}
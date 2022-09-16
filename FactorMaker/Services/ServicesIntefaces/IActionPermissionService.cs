using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IActionPermissionService
    {
        ICollection<ActionPermission> GetAll();
        Task<ICollection<ActionPermission>> GetAllAsync();
        ActionPermission GetById(Guid id);
        Task<ActionPermission> GetByIdAsync(Guid id);
    }
}
using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.ActionPermission;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface IActionPermissionService
    {
        Task<Result<ActionPermissionViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<ActionPermissionViewModel>>> GetAllAsync();
    }
}
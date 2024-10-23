using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.RoleActionPermission;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface IRoleActionPermissionService
    {
        Task<Result<RoleActionPermissionViewModel>> InsertAsync(RoleActionPermissionViewModel viewModel);
        Task<Result<RoleActionPermissionViewModel>> UpdateAsync(RoleActionPermissionViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<RoleActionPermissionViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<RoleActionPermissionViewModel>>> GetAllAsync();
    }
}
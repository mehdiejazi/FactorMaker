using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Role;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface IRoleService
    {
        Task<Result<RoleViewModel>> InsertAsync(RoleViewModel viewModel);
        Task<Result<RoleViewModel>> UpdateAsync(RoleViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<RoleViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<RoleViewModel>>> GetAllAsync();
        Task<Result<RoleViewModel>> SetDefaultRoleAsync(Guid id);
        Task<Result<RoleViewModel>> GetDefaultRoleAsync();

    }
}
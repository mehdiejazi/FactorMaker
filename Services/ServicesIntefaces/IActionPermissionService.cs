using Common;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.ActionPermission;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IActionPermissionService
    {
        Task<Result<ActionPermissionViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<ActionPermissionViewModel>>> GetAllAsync();
    }
}
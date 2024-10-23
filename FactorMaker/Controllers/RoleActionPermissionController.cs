using Common;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using System.Threading.Tasks;
using System;
using ViewModels.RoleActionPermission;
using Microsoft.AspNetCore.Mvc;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services;

namespace FactorMaker.Controllers
{
    public class RoleActionPermissionController : BaseApiController
    {
        private RoleActionPermissionController() : base()
        {

        }
        public RoleActionPermissionController(IRoleActionPermissionService roleActionPermissionService)
        {
            RoleActionPermissionService = roleActionPermissionService;
        }
        private IRoleActionPermissionService RoleActionPermissionService { get; }

        [Authorize]
        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(RoleActionPermissionViewModel viewModel)
        {
            var result = await RoleActionPermissionService.InsertAsync(viewModel);
            return Result(result);
        }

        [Authorize]
        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(RoleActionPermissionViewModel viewModel)
        {
            var result = await RoleActionPermissionService.UpdateAsync(viewModel);
            return Result(result);
        }

        [Authorize]
        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await RoleActionPermissionService.DeleteByIdAsync(id);
            return Result(result);
        }

        [Authorize]
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await RoleActionPermissionService.GetByIdAsync(id);
            return Result(result);
        }

        [Authorize]
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await RoleActionPermissionService.GetAllAsync();
            return Result(result);
        }
    }
}

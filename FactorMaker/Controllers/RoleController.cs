using Common;
using FactorMaker.Services.ServiceIntefaces;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Role;

namespace FactorMaker.Controllers
{
    //[Authorize]
    public class RoleController : BaseApiController
    {
        private RoleController() : base()
        {

        }
        public RoleController(IRoleService roleService)
        {
            RoleService = roleService;
        }
        private IRoleService RoleService { get; }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await RoleService.GetAllAsync();
            return Result(result);
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await RoleService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await RoleService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(RoleViewModel viewModel)
        {
            var result = await RoleService.InsertAsync(viewModel);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(RoleViewModel viewModel)
        {
            var result = await RoleService.UpdateAsync(viewModel);
            return Result(result);
        }

        [HttpGet("SetDefaultRoleAsync")]
        public async Task<IActionResult> SetDefaultRoleAsync(Guid id)
        {
            var result = await RoleService.SetDefaultRoleAsync(id);
            return Result(result);
        }

        [HttpGet("GetDefaultRoleAsync")]
        public async Task<IActionResult> GetDefaultRoleAsync()
        {
            var result = await RoleService.GetDefaultRoleAsync();
            return Result(result);
        }
    }
}

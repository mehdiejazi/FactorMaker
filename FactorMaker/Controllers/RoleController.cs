using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Role;

namespace FactorMaker.Controllers
{
    [Authorize]
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
        public async Task<Result<ICollection<RoleViewModel>>> GetAllAsync()
        {
            var result =  await RoleService.GetAllAsync();
            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var result = await RoleService.DeleteByIdAsync(id);
            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<RoleViewModel>> GetByIdAsync(Guid id)
        {
            var result =  await RoleService.GetByIdAsync(id);
            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<RoleViewModel>> InsertAsync(RoleViewModel viewModel)
        {
            var result = await RoleService.InsertAsync(viewModel);
            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<RoleViewModel>> UpdateAsync(RoleViewModel viewModel)
        {
            var result = await RoleService.UpdateAsync(viewModel);
            return result;
        }

    }
}

using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

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

        [HttpGet("GetAll")]
        public Result<ICollection<Role>> GetAll()
        {
            var result = new Result<ICollection<Role>>();
            result.Data = RoleService.GetAll();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetAllAsync")]
        public async Task<Result<ICollection<Role>>> GetAllAsync()
        {
            var result = new Result<ICollection<Role>>();
            result.Data = await RoleService.GetAllAsync();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("DeleteById")]
        public Result DeleteById(Guid id)
        {
            RoleService.DeleteById(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            await RoleService.DeleteByIdAsync(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetById")]
        public Result<Role> GetById(Guid id)
        {
            var result = new Result<Role>();
            result.Data = RoleService.GetById(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<Role>> GetByIdAsync(Guid id)
        {
            var result = new Result<Role>();
            result.Data = await RoleService.GetByIdAsync(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Insert")]
        public Result<Role> Insert(RoleViewModel roleViewModel)
        {
            var result = new Result<Role>();
            result.Data = RoleService.Insert(roleViewModel.RoleName, roleViewModel.Description);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<Role>> InsertAsync(RoleViewModel roleViewModel)
        {
            var result = new Result<Role>();
            result.Data = await RoleService.InsertAsync(roleViewModel.RoleName, roleViewModel.Description);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Update")]
        public Result<Role> Update(RoleViewModel roleViewModel)
        {
            var result = new Result<Role>();
            result.Data = RoleService.Update(roleViewModel.Id,
                roleViewModel.RoleName, roleViewModel.Description);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<Role>> UpdateAsync(RoleViewModel roleViewModel)
        {
            var result = new Result<Role>();
            result.Data = await RoleService.UpdateAsync(roleViewModel.Id,
                roleViewModel.RoleName, roleViewModel.Description);
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("AddActionPermission")]
        public Result<Role> AddActionPermission(Guid roleId, Guid actionPermissionId)
        {
            var result = new Result<Role>();
            result.Data = RoleService.AddActionPermission(roleId, actionPermissionId);
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("AddActionPermissionAsync")]
        public async Task<Role> AddActionPermissionAsync(Guid roleId, Guid actionPermissionId)
        {
            var result = new Result<Role>();
            result.Data = await RoleService.AddActionPermissionAsync(roleId, actionPermissionId);
            result.IsSuccessful = true;

            return null;
        }
    }
}

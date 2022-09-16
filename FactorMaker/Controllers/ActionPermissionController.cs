using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactorMaker.Controllers
{
    public class ActionPermissionController : BaseApiController
    {
        private ActionPermissionController() : base()
        {

        }
        public ActionPermissionController(IActionPermissionService actionPermissionService)
        {
            ActionPermissionService = actionPermissionService;
        }
        private IActionPermissionService ActionPermissionService { get; }

        [Authorize]
        [HttpGet("GetAll")]
        public Result<ICollection<ActionPermission>> GetAll()
        {
            var result = new Result<ICollection<ActionPermission>>();
            result.Data = ActionPermissionService.GetAll();
            result.IsSuccessful = true;

            return result;
        }

        [Authorize]
        [HttpGet("GetAllAsync")]
        public async Task<Result<ICollection<ActionPermission>>> GetAllAsync()
        {
            var result = new Result<ICollection<ActionPermission>>();
            result.Data = await ActionPermissionService.GetAllAsync();
            result.IsSuccessful = true;

            return result;
        }

        [Authorize]
        [HttpGet("GetById")]
        public Result<ActionPermission> GetById(Guid id)
        {
            var result = new Result<ActionPermission>();
            result.Data = ActionPermissionService.GetById(id);
            result.IsSuccessful = true;

            return result;
        }

        [Authorize]
        [HttpGet("GetByIdAsync")]
        public async Task<Result<ActionPermission>> GetByIdAsync(Guid id)
        {
            var result = new Result<ActionPermission>();
            result.Data = await ActionPermissionService.GetByIdAsync(id);
            result.IsSuccessful = true;

            return result;
        }


    }
}

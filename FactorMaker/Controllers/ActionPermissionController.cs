using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.ActionPermission;

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
        [HttpGet("GetAllAsync")]
        public async Task<Result<ICollection<ActionPermissionViewModel>>> GetAllAsync()
        {
            var result = await ActionPermissionService.GetAllAsync();
            return result;
        }


        [Authorize]
        [HttpGet("GetByIdAsync")]
        public async Task<Result<ActionPermissionViewModel>> GetByIdAsync(Guid id)
        {
            var result = await ActionPermissionService.GetByIdAsync(id);
            return result;
        }


    }
}

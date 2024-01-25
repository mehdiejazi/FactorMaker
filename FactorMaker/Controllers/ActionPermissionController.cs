using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await ActionPermissionService.GetAllAsync();
            return Result(result);
        }

        [Authorize]
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await ActionPermissionService.GetByIdAsync(id);
            return Result(result);
        }
    }
}

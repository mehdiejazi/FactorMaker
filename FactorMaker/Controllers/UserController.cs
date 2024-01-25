using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.User;

namespace FactorMaker.Controllers
{
    public class UserController : BaseApiController
    {
        private UserController() : base()
        {

        }
        public UserController(IUserService userService)
        {
            UserService = userService;
        }
        private IUserService UserService { get; }

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await UserService.DeleteByIdAsync(id);
            return Result(result);
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await UserService.GetAllAsync();
            return Result(result);
        }

        [HttpGet("GetActiveAsync")]
        public async Task<IActionResult> GetActiveAsync()
        {
            var result =  await UserService.GetActiveAsync();
            return Result(result);
        }

        [HttpGet("GetInActiveAsync")]
        public async Task<IActionResult> GetInActiveAsync()
        {
            var result =  await UserService.GetInActiveAsync();
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result =  await UserService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(UserViewModel viewModel)
        {
            var result = await UserService.InsertAsync(viewModel);
            return Result(result);
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(UserViewModel viewModel)
        {
            var result = await UserService.UpdateAsync(viewModel);
            return Result(result);
        }

    }
}

using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.User;

namespace FactorMaker.Controllers
{
    public class UserController : BaseApiControllerWithUser
    {
        private UserController() : base()
        {

        }
        public UserController(IUserService userService)
        {
            UserService = userService;
        }
        private IUserService UserService { get; }

        [Authorize]

        [HttpGet("DeleteByIdAsync")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await UserService.DeleteByIdAsync(id);
            return Result(result);
        }

        [Authorize]

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await UserService.GetAllAsync();
            return Result(result);
        }

        [Authorize]

        [HttpGet("GetActiveAsync")]
        public async Task<IActionResult> GetActiveAsync()
        {
            var result =  await UserService.GetActiveAsync();
            return Result(result);
        }

        [Authorize]

        [HttpGet("GetInActiveAsync")]
        public async Task<IActionResult> GetInActiveAsync()
        {
            var result =  await UserService.GetInActiveAsync();
            return Result(result);
        }

        [Authorize]

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result =  await UserService.GetByIdAsync(id);
            return Result(result);
        }

        [Authorize]

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(UserViewModel viewModel)
        {
            var result = await UserService.InsertAsync(viewModel);
            return Result(result);
        }

        [Authorize]

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(UserViewModel viewModel)
        {
            var result = await UserService.UpdateAsync(viewModel);
            return Result(result);
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]

        [HttpPost("RegisterAsync")]
        public async Task<IActionResult> RegisterAsync(UserRegisterViewModel viewModel)
        {
            var result = await UserService.RegisterAsync(viewModel);
            return Result(result);
        }

        [Authenticated]

        [HttpPost("ChangePasswordAsync")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel viewModel)
        {
            if (User.Role?.Name != "Programmer")
            {
                // non-admin users may only change their own password
                viewModel.UserId = User.Id;
            }

            var result = await UserService.ChangePasswordAsync(viewModel);
            return Result(result);
        }
    }
}

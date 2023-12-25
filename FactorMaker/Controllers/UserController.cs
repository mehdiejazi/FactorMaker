using Common;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Authentication;
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
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var result = await UserService.DeleteByIdAsync(id);
            return result;
        }

        [HttpGet("GetAllAsync")]
        public async Task<Result<ICollection<UserViewModel>>> GetAllAsync()
        {
            var result = await UserService.GetAllAsync();
            return result;
        }

        [HttpGet("GetActiveAsync")]
        public async Task<Result<ICollection<UserViewModel>>> GetActiveAsync()
        {
            var result =  await UserService.GetActiveAsync();
            return result;
        }

        [HttpGet("GetInActiveAsync")]
        public async Task<Result<ICollection<UserViewModel>>> GetInActiveAsync()
        {
            var result =  await UserService.GetInActiveAsync();
            return result;
        }


        [HttpGet("GetByIdAsync")]
        public async Task<Result<UserViewModel>> GetByIdAsync(Guid id)
        {
            var result =  await UserService.GetByIdAsync(id);
            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<UserViewModel>> InsertAsync(UserViewModel viewModel)
        {
            var result = await UserService.InsertAsync(viewModel);
            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<UserViewModel>> UpdateAsync(UserViewModel viewModel)
        {
            var result = await UserService.UpdateAsync(viewModel);
            return result;
        }

    }
}

using Common;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;
using User = Models.User;

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

        [HttpGet("DeleteById")]
        public Result DeleteById(Guid id)
        {
            UserService.DeleteById(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            await UserService.DeleteByIdAsync(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetAll")]
        public Result<ICollection<User>> GetAll()
        {
            var result = new Result<ICollection<User>>();
            result.Data = UserService.GetAll();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetAllAsync")]
        public async Task<Result<ICollection<User>>> GetAllAsync()
        {
            var result = new Result<ICollection<User>>();
            result.Data = await UserService.GetAllAsync();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetById")]
        public Result<User> GetById(Guid id)
        {
            var result = new Result<User>();
            result.Data = UserService.GetById(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<User>> GetByIdAsync(Guid id)
        {
            var result = new Result<User>();
            result.Data = await UserService.GetByIdAsync(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Insert")]
        public Result<UserViewModel> Insert(UserViewModel userViewModel)
        {

            var user = UserService.Insert(userViewModel.FirstName, userViewModel.LastName, userViewModel.NationalCode,
                userViewModel.UserName, userViewModel.Password, userViewModel.IsActive);

            var returnedUserViewModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                InsertDateTime = user.InsertDateTime,
                IsActive = user.IsActive,
                NationalCode = user.NationalCode,
                UserName = user.UserName,

            };

            var result = new Result<UserViewModel>();
            result.Data = returnedUserViewModel;
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<UserViewModel>> InsertAsync(UserViewModel userViewModel)
        {
            var user = await UserService.InsertAsync(userViewModel.FirstName, userViewModel.LastName, userViewModel.NationalCode,
                userViewModel.UserName, userViewModel.Password, userViewModel.IsActive);

            var returnedUserViewModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                InsertDateTime = user.InsertDateTime,
                IsActive = user.IsActive,
                NationalCode = user.NationalCode,
                UserName = user.UserName,

            };

            var result = new Result<UserViewModel>();
            result.Data = returnedUserViewModel;
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Update")]
        public Result<UserViewModel> Update(UserViewModel userViewModel)
        {
            var user = UserService.Update(userViewModel.Id, userViewModel.FirstName, userViewModel.LastName,
                userViewModel.NationalCode, userViewModel.UserName, userViewModel.Password, userViewModel.IsActive);

            var returnedUserViewModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                InsertDateTime = user.InsertDateTime,
                IsActive = user.IsActive,
                NationalCode = user.NationalCode,
                UserName = user.UserName,

            };

            var result = new Result<UserViewModel>();
            result.Data = returnedUserViewModel;
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<UserViewModel>> UpdateAsync(UserViewModel userViewModel)
        {
            var returneUserViewModel = await UserService.UpdateAsync(userViewModel.Id, userViewModel.FirstName, userViewModel.LastName,
                userViewModel.NationalCode, userViewModel.UserName, userViewModel.Password, userViewModel.IsActive);

            var result = new Result<UserViewModel>();
            result.Data = returneUserViewModel;
            result.IsSuccessful = true;

            return result;
        }

    }
}

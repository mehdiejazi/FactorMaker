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

        [HttpPost("Login")]
        public Result<LoginResponseViewModel> Login(LoginRequestViewModel loginRequest)
        {
            var response = UserService.Login(loginRequest);

            var result = new Result<LoginResponseViewModel>();
            result.Data = response;
            result.IsSuccessful = true;

            if (response == null)
            {
                result.IsSuccessful = false;
                result.AddErrorMessage(Resources.ErrorMessages.LoginFailed);
            }

            return result;
        }

        [HttpPost("LoginAsync")]
        public async Task<Result<LoginResponseViewModel>> LoginAsync(LoginRequestViewModel loginRequest)
        {
            var response = await UserService.LoginAsync(loginRequest);

            var result = new Result<LoginResponseViewModel>();
            result.Data = response;
            result.IsSuccessful = true;

            if (response == null)
            {
                result.IsSuccessful = false;
                result.AddErrorMessage(Resources.ErrorMessages.LoginFailed);
            }

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpGet("DeleteById")]
        public Result DeleteById(Guid id)
        {
            UserService.DeleteById(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            await UserService.DeleteByIdAsync(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpGet("GetAll")]
        public Result<ICollection<User>> GetAll()
        {
            var result = new Result<ICollection<User>>();
            result.Data = UserService.GetAll();
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpGet("GetAllAsync")]
        public async Task<Result<ICollection<User>>> GetAllAsync()
        {
            var result = new Result<ICollection<User>>();
            result.Data = await UserService.GetAllAsync();
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpGet("GetActive")]
        public Result<ICollection<User>> GetActive()
        {
            var result = new Result<ICollection<User>>();
            result.Data = UserService.GetActive();
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpGet("GetActiveAsync")]
        public async Task<Result<ICollection<User>>> GetActiveAsync()
        {
            var result = new Result<ICollection<User>>();
            result.Data = await UserService.GetActiveAsync();
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpGet("GetInActive")]
        public Result<ICollection<User>> GetInActive()
        {
            var result = new Result<ICollection<User>>();
            result.Data = UserService.GetInActive();
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpGet("GetInActiveAsync")]
        public async Task<Result<ICollection<User>>> GetInActiveAsync()
        {
            var result = new Result<ICollection<User>>();
            result.Data = await UserService.GetInActiveAsync();
            result.IsSuccessful = true;

            return result;
        }


        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpGet("GetById")]
        public Result<User> GetById(Guid id)
        {
            var result = new Result<User>();
            result.Data = UserService.GetById(id);
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpGet("GetByIdAsync")]
        public async Task<Result<User>> GetByIdAsync(Guid id)
        {
            var result = new Result<User>();
            result.Data = await UserService.GetByIdAsync(id);
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpPost("Insert")]
        public Result<UserViewModel> Insert(UserViewModel userViewModel)
        {

            var user = UserService.Insert(userViewModel.FirstName, userViewModel.LastName, userViewModel.NationalCode,
                userViewModel.UserName, userViewModel.Password, userViewModel.IsActive, userViewModel.RoleId);

            var returnedUserViewModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                InsertDateTime = user.InsertDateTime,
                IsActive = user.IsActive,
                NationalCode = user.NationalCode,
                UserName = user.UserName,
                RoleId = user.Role.Id,
                RoleName = user.Role.RolName,
            };

            var result = new Result<UserViewModel>();
            result.Data = returnedUserViewModel;
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpPost("InsertAsync")]
        public async Task<Result<UserViewModel>> InsertAsync(UserViewModel userViewModel)
        {
            var user = await UserService.InsertAsync(userViewModel.FirstName, userViewModel.LastName, userViewModel.NationalCode,
                userViewModel.UserName, userViewModel.Password, userViewModel.IsActive,userViewModel.RoleId);

            var returnedUserViewModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                InsertDateTime = user.InsertDateTime,
                IsActive = user.IsActive,
                NationalCode = user.NationalCode,
                UserName = user.UserName,
                RoleId = user.Role.Id,
                RoleName = user.Role.RolName,
            };

            var result = new Result<UserViewModel>();
            result.Data = returnedUserViewModel;
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpPost("Update")]
        public Result<UserViewModel> Update(UserViewModel userViewModel)
        {
            var user = UserService.Update(userViewModel.Id, userViewModel.FirstName, userViewModel.LastName,
                userViewModel.NationalCode, userViewModel.UserName, userViewModel.Password, 
                userViewModel.IsActive, userViewModel.RoleId);

            var returnedUserViewModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                InsertDateTime = user.InsertDateTime,
                IsActive = user.IsActive,
                NationalCode = user.NationalCode,
                UserName = user.UserName,
                RoleId = user.Role.Id,
                RoleName = user.Role.RolName,
            };

            var result = new Result<UserViewModel>();
            result.Data = returnedUserViewModel;
            result.IsSuccessful = true;

            return result;
        }

        //[Authorize(RoleType = RoleType.Administrator)]
        [HttpPost("UpdateAsync")]
        public async Task<Result<UserViewModel>> UpdateAsync(UserViewModel userViewModel)
        {
            var user = await UserService.UpdateAsync(userViewModel.Id, userViewModel.FirstName, userViewModel.LastName,
                userViewModel.NationalCode, userViewModel.UserName, userViewModel.Password, 
                userViewModel.IsActive, userViewModel.RoleId);

            var returnedUserViewModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                InsertDateTime = user.InsertDateTime,
                IsActive = user.IsActive,
                NationalCode = user.NationalCode,
                UserName = user.UserName,
                RoleId = user.Role.Id,
                RoleName = user.Role.RolName,
            };

            var result = new Result<UserViewModel>();
            result.Data = returnedUserViewModel;
            result.IsSuccessful = true;

            return result;
        }

    }
}

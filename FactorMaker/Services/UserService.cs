using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.User;

namespace FactorMaker.Services
{
    public class UserService : BaseServiceWithDatabase, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Result<UserViewModel>> InsertAsync(UserViewModel viewModel)
        {
            try
            {
                var result = new Result<UserViewModel>();
                result.IsSuccessful = true;

                Role role = await UnitOfWork.RoleRepository.GetByIdAsync(viewModel.RoleId);
                if (role == null)
                {
                    result.AddErrorMessage(typeof(Role) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                User user = viewModel.Adapt<User>();

                user.Password = Utilities.HashSHA1(user.Password);

                await UnitOfWork.UserRepository.InsertAsync(user);
                await UnitOfWork.SaveAsync();

                result.Data = user.Adapt<UserViewModel>();
                result.IsSuccessful = true;

                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Result<UserViewModel>> UpdateAsync(UserViewModel viewModel)
        {
            try
            {
                var result = new Result<UserViewModel>();
                result.IsSuccessful = true;

                var user = await UnitOfWork.UserRepository.GetByIdAsync(viewModel.Id);
                if (user == null)
                {
                    result.AddErrorMessage(typeof(User) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                Role role = await UnitOfWork.RoleRepository.GetByIdAsync(viewModel.RoleId);
                if (role == null)
                {
                    result.AddErrorMessage(typeof(Role) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.NationalCode = viewModel.NationalCode;
                user.UserName = viewModel.UserName;
                user.Password = Utilities.HashSHA1(viewModel.Password);
                user.IsActive = viewModel.IsActive;

                await UnitOfWork.UserRepository.UpdateAsync(user);
                await UnitOfWork.SaveAsync();

                result.Data = user.Adapt<UserViewModel>();
                result.IsSuccessful = true;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            try
            {
                Result result = new Result();
                result.IsSuccessful = true;

                User user = await UnitOfWork.UserRepository.GetByIdAsync(id);
                if (user == null)
                {
                    result.AddErrorMessage(typeof(User) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                await UnitOfWork.UserRepository.DeleteAsync(user);
                await UnitOfWork.SaveAsync();

                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<UserViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<UserViewModel>();
                result.IsSuccessful = true;

                User user = await UnitOfWork.UserRepository.GetByIdAsync(id);
                if (user == null)
                {
                    result.AddErrorMessage(typeof(User) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = user.Adapt<UserViewModel>();
                result.IsSuccessful = true;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<UserViewModel>>> GetAllAsync()
        {
            try
            {
                var result = new Result<ICollection<UserViewModel>>();
                result.IsSuccessful = true;

                var users = await UnitOfWork.UserRepository.GetAllAsync();

                result.Data = users.Adapt<ICollection<UserViewModel>>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<UserViewModel>>> GetActiveAsync()
        {
            try
            {
                var result = new Result<ICollection<UserViewModel>>();
                result.IsSuccessful = true;

                var users = await UnitOfWork.UserRepository.GetActiveAsync();

                result.Data = users.Adapt<ICollection<UserViewModel>>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<UserViewModel>>> GetInActiveAsync()
        {
            try
            {
                var result = new Result<ICollection<UserViewModel>>();
                result.IsSuccessful = true;

                var users = await UnitOfWork.UserRepository.GetInActiveAsync();

                result.Data = users.Adapt<ICollection<UserViewModel>>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

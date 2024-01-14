using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Flurl.Util;
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

                
                if (await UnitOfWork.UserRepository.IsExistByUsernameAsync(viewModel.UserName))
                {
                    result.IsSuccessful = false;
                    result.AddErrorMessage(ErrorMessages.UsernameAlreadyExists);
                    return result;
                }


                Role role = await UnitOfWork.RoleRepository.GetByIdAsync(viewModel.RoleId);
                if (role == null)
                {
                    result.AddErrorMessage(typeof(Role) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                var user = viewModel.Adapt<User>();

                user.Password = Utilities.HashSHA1(user.Password);

                await UnitOfWork.UserRepository.InsertAsync(user);
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
        public async Task<Result<UserViewModel>> RegisterAsync(UserRegisterViewModel viewModel)
        {
            try
            {
                var result = new Result<UserViewModel>();

                var user = await UnitOfWork.UserRepository.GetByUserNameAsync(viewModel.UserName);
                if (user != null)
                {
                    result.IsSuccessful = false;
                    result.AddErrorMessage(ErrorMessages.UsernameAlreadyExists);
                    return result;
                }

                user = new User()
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    UserName = viewModel.UserName,
                    Password = Utilities.HashSHA1(viewModel.Password)
                };

                var insertResult = await InsertAsync(user.Adapt<UserViewModel>());

                foreach (var item in insertResult.InformationMessages)
                {
                    result.AddInformationMessage(item);
                }
                foreach (var item in insertResult.WarningMessages)
                {
                    result.AddInformationMessage(item);
                }

                if (insertResult.IsSuccessful == false)
                {
                    foreach (var item in insertResult.ErrorMessages)
                    {
                        result.AddErrorMessage(item);
                    }
                    result.IsSuccessful = false;
                }
                else
                {
                    result.Data = insertResult.Data;
                    result.IsSuccessful = true;
                }


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

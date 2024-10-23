using Common;
using Data;
using FactorMaker.Infrastructure.ApplicationSettings;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServiceIntefaces;
using Mapster;
using Microsoft.Extensions.Caching.Memory;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Store;
using ViewModels.User;

namespace FactorMaker.Services
{
    public class UserService : BaseServiceWithDatabase, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMemoryCache memoryCache, AuthSettings authSettings) : base(unitOfWork)
        {
            MemoryCache = memoryCache;
            AuthSettings = authSettings;
        }
        protected IMemoryCache MemoryCache { get; }
        protected AuthSettings AuthSettings { get; }
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

                //user.Password = Utilities.HashSHA1(user.Password);

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

                ImageAsset avatar = await UnitOfWork.ImageAssetRepository.GetByIdAsync(viewModel.AvatarId);

                if (result.IsSuccessful == false) return result;

                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.NationalCode = viewModel.NationalCode;
                user.UserName = viewModel.UserName;
                user.IsActive = viewModel.IsActive;
                user.EmailIsVerified = viewModel.EmailIsVerified;
                user.IsForcedChangePassword = viewModel.IsForcedChangePassword;
                if (avatar != null)
                    user.Avatar = avatar;

                foreach (StoreViewModel vmstore in viewModel.Stores)
                {
                    if (user.Stores.Where(i => i.Id == vmstore.Id).Count() == 0)
                        user.Stores.Add(vmstore.Adapt<Store>());
                }

                for (int i = user.Stores.Count - 1; i >= 0; i--)
                {
                    if (viewModel.Stores.Where(x => x.Id == user.Stores.ToList()[i].Id).Count() == 0)
                    {
                        var store = user.Stores.ToList()[i];
                        user.Stores.Remove(store);
                    }
                }


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

                var defaultRole = await UnitOfWork.RoleRepository.GetDefaultRoleAsync();
                if (defaultRole == null)
                {
                    result.IsSuccessful = false;
                    result.AddErrorMessage(ErrorMessages.NoDefaultRole);
                    return result;
                }


                if (await UnitOfWork.UserRepository.IsExistByUsernameAsync(viewModel.UserName))
                {
                    result.IsSuccessful = false;
                    result.AddErrorMessage(ErrorMessages.UsernameAlreadyExists);
                    return result;
                }

                var user = new User()
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    UserName = viewModel.UserName,
                    Password = Utilities.HashSHA1(viewModel.Password),
                    RoleId = defaultRole.Id,
                    IsActive = true,
                };

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
        public async Task<User> GetByIdForLoginAsync(Guid id)
        {
            try
            {
                var userCachKey = $"userId-{id}";
                User foundUser = null;

                if (MemoryCache.TryGetValue(userCachKey, out foundUser))
                {
                    return foundUser;
                }
                else
                {
                    foundUser = await UnitOfWork.UserRepository.GetByIdAsync(id);

                    var catchEntryOption =
                        new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(AuthSettings.RefreshTokenExpiresInMinutes - 10));

                    MemoryCache.Set(userCachKey, foundUser, catchEntryOption);

                    return foundUser;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> SetRefreshTokenAsync(Guid userId, string refreshToken)
        {
            try
            {
                User user = await UnitOfWork.UserRepository.GetByIdAsync(userId);
                if (user == null) return false;

                UserRefreshToken userRefreshToken;
                if (user.RefreshToken == null)
                {
                    userRefreshToken = new UserRefreshToken();
                    user.RefreshToken = userRefreshToken;
                }
                else
                {
                    userRefreshToken = user.RefreshToken;
                }

                userRefreshToken.RefreshToken = refreshToken;
                userRefreshToken.RefreshTokenTimeOut = AuthSettings.RefreshTokenExpiresInMinutes;
                userRefreshToken.IsValid = true;

                await UnitOfWork.SaveAsync();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Result<UserViewModel>> ChangePasswordAsync(ChangePasswordViewModel viewModel)
        {
            try
            {
                var result = new Result<UserViewModel>();
                result.IsSuccessful = true;

                User user = await UnitOfWork.UserRepository.GetByIdAsync(viewModel.UserId);
                if (user == null)
                {
                    result.AddErrorMessage(typeof(User) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                user.Password = Utilities.HashSHA1(viewModel.Password);
                user.IsForcedChangePassword = false;

                await UnitOfWork.UserRepository.UpdateAsync(user);
                await UnitOfWork.SaveAsync();

                result.Data = user.Adapt<UserViewModel>();
                result.IsSuccessful = true;

                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Common;
using Data;
using FactorMaker.Infrastructure;
using FactorMaker.Infrastructure.ApplicationSettings;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Threading.Tasks;
using ViewModels.Authentication;
using ViewModels.User;

namespace FactorMaker.Services
{
    public class AuthService : BaseServiceWithDatabase, IAuthService
    {
        protected AuthSettings AuthSettings { get; }
        protected IUserService UserService { get; }
        public AuthService(IUnitOfWork unitOfWork, AuthSettings authSettings, UserService userService) : base(unitOfWork)
        {
            AuthSettings = authSettings;
            UserService = userService;
        }
        public async Task<Result<LoginResponseViewModel>> LoginAsync(LoginRequestViewModel loginRequest)
        {
            var result = new Result<LoginResponseViewModel>();
            result.IsSuccessful = true;


            if (string.IsNullOrWhiteSpace(loginRequest.UserName))
            {
                result.AddErrorMessage(Resources.ErrorMessages.UserNameIsEmpty);
                result.IsSuccessful = false;
            }
            if (string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                result.AddErrorMessage(Resources.ErrorMessages.PasswordIsEmpty);
                result.IsSuccessful = false;
            }

            if (result.IsSuccessful == false) return result;

            User foundUser = await UnitOfWork.UserRepository.GetByUserNameAsync(loginRequest.UserName);
            if (foundUser == null)
            {
                result.AddErrorMessage(ErrorMessages.LoginFailed);
                result.IsSuccessful = false;
                return result;
            }

            if (string.Compare(foundUser.Password, Utilities.HashSHA1(loginRequest.Password), false) != 0)
            {
                result.AddErrorMessage(ErrorMessages.LoginFailed);
                result.IsSuccessful = false;
                return result;
            }

            if (foundUser.IsDeleted)
            {
                result.AddErrorMessage(ErrorMessages.LoginFailed);
                result.IsSuccessful = false;
                return result;
            }

            if (foundUser.IsActive == false)
            {
                result.AddErrorMessage(ErrorMessages.UserIsInactive);
                result.IsSuccessful = false;
                return result;
            }

            string token = JwtUtility.GenerateJwtToken(foundUser, AuthSettings);

            result.Data = new LoginResponseViewModel(foundUser.Adapt<UserViewModel>(), token);
            result.IsSuccessful = false;

            return result;
        }
        public async Task<Result<NewTokenResponseViewModel>> GenerateNewTokenAsync(NewTokenRequestViewModel request)
        {
            try
            {
                var result = new Result<NewTokenResponseViewModel>();
                result.IsSuccessful = true;

                User user = await UnitOfWork.UserRepository.GetByRefreshTokenAsync(request.RefreshToken);
                if (user == null)
                {
                    result.AddErrorMessage(typeof(User) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }
                if (user.Id.Equals(request.UserId) == false)
                {
                    result.AddErrorMessage(ErrorMessages.RefreshTokenIsInvalid);
                    result.IsSuccessful = false;
                    return result;
                }
                if (user.RefreshToken == null)
                {
                    result.AddErrorMessage(ErrorMessages.RefreshTokenNotFound);
                    result.IsSuccessful = false;
                }
                if (user.RefreshToken.IsValid == false)
                {
                    result.AddErrorMessage(ErrorMessages.RefreshTokenIsInvalid);
                    result.IsSuccessful = false;
                }
                if (user.RefreshToken.InsertDateTime
                    .AddMinutes(user.RefreshToken.RefreshTokenTimeOut)
                    .CompareTo(DateTime.Now) > 0)
                {
                    result.AddErrorMessage(ErrorMessages.RefreshTokenIsExpired);
                    result.IsSuccessful = false;
                }

                string newToken = JwtUtility.GenerateJwtToken(user, AuthSettings);
                string newRefreshToken = JwtUtility.GenetrateRefreshToken();

                await UserService.SetRefreshTokenAsync(user.Id, newRefreshToken);

                if (result.IsSuccessful == false) return result;

                var response = new NewTokenResponseViewModel()
                {
                    RefreshToken = newRefreshToken,
                    Token = newToken
                };
                result.Data = response;
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

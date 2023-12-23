using Common;
using Data;
using FactorMaker.Infrastructure;
using FactorMaker.Infrastructure.ApplicationSettings;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Models;
using Resources;
using System.Threading.Tasks;
using ViewModels.Authentication;
using ViewModels.User;

namespace FactorMaker.Services
{
    public class AuthService : BaseServiceWithDatabase, IAuthService
    {
        protected AuthSettings AuthSettings { get; }
        public AuthService(IUnitOfWork unitOfWork, AuthSettings authSettings) : base(unitOfWork)
        {
            AuthSettings = authSettings;
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
    }
}

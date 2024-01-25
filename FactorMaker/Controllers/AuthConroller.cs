using Common;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModels.Authentication;
using ViewModels.User;

namespace FactorMaker.Controllers
{
    public class AuthConroller : BaseApiController
    {
        private AuthConroller() : base()
        {

        }
        public AuthConroller(IAuthService authService,IUserService userService)
        {
            AuthService = authService;
            UserService = userService;
        }
        private IAuthService AuthService { get; }
        private IUserService UserService { get; }

        [HttpPost("LoginAsync")]
        public async Task<IActionResult> LoginAsync(LoginRequestViewModel loginRequest)
        {
            var result = await AuthService.LoginAsync(loginRequest);
            return Result(result);
        }

        [HttpPost("GenerateNewTokenAsync")]
        public async Task<Result> GenerateNewTokenAsync([FromBody] NewTokenRequestViewModel request)
        {
            var result = await AuthService.GenerateNewTokenAsync(request);
            return result;
        }
    }
}

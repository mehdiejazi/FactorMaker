using Common;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModels.Authentication;
using ViewModels.User;

namespace FactorMaker.Controllers
{
    public class AuthController : BaseApiController
    {
        private AuthController() : base()
        {

        }
        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }
        private IAuthService AuthService { get; }

        [HttpPost("LoginAsync")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestViewModel loginRequest)
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

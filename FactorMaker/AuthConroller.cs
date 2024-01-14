using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModels.Authentication;

namespace FactorMaker.Controllers
{
    public class AuthConroller : BaseApiController
    {
        private AuthConroller() : base()
        {

        }
        public AuthConroller(IAuthService authService)
        {
            AuthService = authService;
        }
        private IAuthService AuthService { get; }

        [HttpPost("LoginAsync")]
        public async Task<IActionResult> LoginAsync(LoginRequestViewModel loginRequest)
        {
            var result = await AuthService.LoginAsync(loginRequest);
            return Result(result);
        }
    }
}

using Common;
using System.Threading.Tasks;
using ViewModels.Authentication;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IAuthService
    {
        Task<Result<LoginResponseViewModel>> LoginAsync(LoginRequestViewModel loginRequest);
    }
}
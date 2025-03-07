﻿using Common;
using System.Threading.Tasks;
using ViewModels.Authentication;
using ViewModels.User;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface IAuthService
    {
        Task<Result<NewTokenResponseViewModel>> GenerateNewTokenAsync(NewTokenRequestViewModel request);
        Task<Result<LoginResponseViewModel>> LoginAsync(LoginRequestViewModel loginRequest);
    }
}
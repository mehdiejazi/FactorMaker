﻿using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.User;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface IUserService
    {
        Task<Result<UserViewModel>> InsertAsync(UserViewModel viewModel);
        Task<Result<UserViewModel>> UpdateAsync(UserViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<UserViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<UserViewModel>>> GetAllAsync();
        Task<Result<ICollection<UserViewModel>>> GetActiveAsync();
        Task<Result<ICollection<UserViewModel>>> GetInActiveAsync();
        Task<User> GetByIdForLoginAsync(Guid id);
        Task<bool> SetRefreshTokenAsync(Guid userId, string refreshToken);
        Task<Result<UserViewModel>> RegisterAsync(UserRegisterViewModel viewModel);
        Task<Result<UserViewModel>> ChangePasswordAsync(ChangePasswordViewModel viewModel);
    }
}
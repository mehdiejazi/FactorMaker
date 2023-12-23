using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.User;

namespace FactorMaker.Services.ServicesIntefaces
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
    }
}
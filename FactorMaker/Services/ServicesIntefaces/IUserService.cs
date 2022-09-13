using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Authentication;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IUserService
    {
        LoginResponseViewModel Login(LoginRequestViewModel loginRequest);
        Task<LoginResponseViewModel> LoginAsync(LoginRequestViewModel loginRequest);
        void DeleteById(Guid id);
        Task DeleteByIdAsync(Guid id);
        ICollection<User> GetAll();
        Task<ICollection<User>> GetAllAsync();
        User GetById(Guid id);
        Task<User> GetByIdAsync(Guid id);
        User Insert(string firstName, string lastName, string nationalCode, string userName, string password, bool isActive);
        Task<User> InsertAsync(string firstName, string lastName, string nationalCode, string userName, string password, bool isActive);
        UserViewModel Update(Guid id, string firstName, string lastName, string nationalCode, string userName, string password, bool isActive);
        Task<UserViewModel> UpdateAsync(Guid id, string firstName, string lastName, string nationalCode, string userName, string password, bool isActive);
    }
}
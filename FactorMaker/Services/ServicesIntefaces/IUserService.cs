using Models;
using Models.Enums;
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
        ICollection<User> GetActive();
        Task<ICollection<User>> GetActiveAsync();
        ICollection<User> GetInActive();
        Task<ICollection<User>> GetInActiveAsync();
        User GetById(Guid id);
        Task<User> GetByIdAsync(Guid id);
        User Insert(string firstName, string lastName, string nationalCode,
            string userName, string password, bool isActive, Guid roleId);
        Task<User> InsertAsync(string firstName, string lastName, string nationalCode,
            string userName, string password, bool isActive, Guid roleId);
        User Update(Guid id, string firstName, string lastName, string nationalCode,
            string userName, string password, bool isActive, Guid roleId);
        Task<User> UpdateAsync(Guid id, string firstName, string lastName, string nationalCode,
            string userName, string password, bool isActive, Guid roleId);
    }
}
﻿using FactorMaker.Services.Base;
using Data;
using Models;
using System;
using System.Threading.Tasks;
using Resources;
using System.Collections.Generic;
using ViewModels;
using FactorMaker.Services.ServicesIntefaces;
using ViewModels.Authentication;
using Microsoft.Extensions.Options;
using FactorMaker.Infrastructure.ApplicationSettings;
using FactorMaker.Infrastructure;

namespace FactorMaker.Services
{
    public class UserService : BaseServiceWithDatabase, IUserService
    {
        protected Main MainSettings { get; }
        public UserService(IUnitOfWork unitOfWork, IOptions<Main> options) : base(unitOfWork)
        {
            MainSettings = options.Value;
        }

        public LoginResponseViewModel Login(LoginRequestViewModel loginRequest)
        {
            if (loginRequest == null) return null;

            if (string.IsNullOrWhiteSpace(loginRequest.UserName)) return null;
            if (string.IsNullOrWhiteSpace(loginRequest.Password)) return null;

            User foundUser = UnitOfWork.UserRepository.GetByUserName(loginRequest.UserName);
            if (foundUser == null) return null;

            if (string.Compare(foundUser.Password, JwtUtility.HashSHA1(loginRequest.Password), false) != 0)
                return null;

            string token = JwtUtility.GenerateJwtToken(foundUser, MainSettings);

            var loginResponse = new LoginResponseViewModel(foundUser, token);

            return loginResponse;
        }

        public async Task<LoginResponseViewModel> LoginAsync(LoginRequestViewModel loginRequest)
        {
            if (loginRequest == null) return null;

            if (string.IsNullOrWhiteSpace(loginRequest.UserName)) return null;
            if (string.IsNullOrWhiteSpace(loginRequest.Password)) return null;

            User foundUser = await UnitOfWork.UserRepository.GetByUserNameAsync(loginRequest.UserName);
            if (foundUser == null) return null;

            if (string.Compare(foundUser.Password, loginRequest.Password, false) != 0)
                return null;

            string token = JwtUtility.GenerateJwtToken(foundUser, MainSettings);

            var loginResponse = new LoginResponseViewModel(foundUser, token);

            return loginResponse;
        }


        public User Insert(string firstName, string lastName, string nationalCode,
            string userName, string password, bool isActive)
        {
            try
            {
                User user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    NationalCode = nationalCode,
                    UserName = userName,
                    Password = password,
                    IsActive = isActive

                };

                user.Password = JwtUtility.HashSHA1(user.Password);

                UnitOfWork.UserRepository.Insert(user);
                UnitOfWork.Save();

                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<User> InsertAsync(string firstName, string lastName, string nationalCode,
            string userName, string password, bool isActive)
        {
            try
            {
                User user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    NationalCode = nationalCode,
                    UserName = userName,
                    Password = password,
                    IsActive = isActive

                };

                user.Password = JwtUtility.HashSHA1(user.Password);


                await UnitOfWork.UserRepository.InsertAsync(user);
                await UnitOfWork.SaveAsync();

                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UserViewModel Update(Guid id, string firstName, string lastName, string nationalCode,
            string userName, string password, bool isActive)
        {
            try
            {
                User user = UnitOfWork.UserRepository.GetById(id);

                if (user == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                user.FirstName = firstName;
                user.LastName = lastName;
                user.NationalCode = nationalCode;
                user.UserName = userName;
                user.Password = password;
                user.IsActive = isActive;

                UnitOfWork.UserRepository.Update(user);
                UnitOfWork.Save();

                var returnedUserViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    InsertDateTime = user.InsertDateTime,
                    IsActive = user.IsActive,
                    NationalCode = user.NationalCode,
                    UserName = user.UserName,

                };

                return returnedUserViewModel;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<UserViewModel> UpdateAsync(Guid id, string firstName, string lastName, string nationalCode,
            string userName, string password, bool isActive)
        {
            try
            {
                User user = UnitOfWork.UserRepository.GetById(id);

                if (user == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                user.FirstName = firstName;
                user.LastName = lastName;
                user.NationalCode = nationalCode;
                user.UserName = userName;
                user.Password = password;
                user.IsActive = isActive;

                await UnitOfWork.UserRepository.UpdateAsync(user);
                await UnitOfWork.SaveAsync();

                var returnedUserViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    InsertDateTime = user.InsertDateTime,
                    IsActive = user.IsActive,
                    NationalCode = user.NationalCode,
                    UserName = user.UserName,

                };

                return returnedUserViewModel;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteById(Guid id)
        {
            try
            {
                User user = UnitOfWork.UserRepository.GetById(id);

                if (user == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                UnitOfWork.UserRepository.Delete(user);
                UnitOfWork.Save();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            try
            {
                User user = UnitOfWork.UserRepository.GetById(id);

                if (user == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                await UnitOfWork.UserRepository.DeleteAsync(user);
                await UnitOfWork.SaveAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public User GetById(Guid id)
        {
            try
            {
                User user = UnitOfWork.UserRepository.GetById(id);

                if (user == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                return user;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<User> GetByIdAsync(Guid id)
        {
            try
            {
                User user = await UnitOfWork.UserRepository.GetByIdAsync(id);

                if (user == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                return user;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ICollection<User> GetAll()
        {
            try
            {
                var users = UnitOfWork.UserRepository.GetAll();

                return users;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            try
            {
                var users = await UnitOfWork.UserRepository.GetAllAsync();

                return users;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

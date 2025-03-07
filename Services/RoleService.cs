﻿using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Role;

namespace FactorMaker.Services
{
    public class RoleService : BaseServiceWithDatabase, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Result<RoleViewModel>> InsertAsync(RoleViewModel viewModel)
        {
            try
            {
                var result = new Result<RoleViewModel>();

                var role = viewModel.Adapt<Role>();

                await UnitOfWork.RoleRepository.InsertAsync(role);
                await UnitOfWork.SaveAsync();

                result.Data = role.Adapt<RoleViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<RoleViewModel>> UpdateAsync(RoleViewModel viewModel)
        {
            try
            {
                var result = new Result<RoleViewModel>();
                result.IsSuccessful = true;

                Role role = await UnitOfWork.RoleRepository.GetByIdAsync(viewModel.Id);
                if (role == null)
                {
                    result.AddErrorMessage(typeof(Role) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                role.RolName = viewModel.RoleName;
                role.Description = viewModel.Description;


                UnitOfWork.RoleRepository.Update(role);
                UnitOfWork.Save();

                result.Data = role.Adapt<RoleViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = new Result();
                result.IsSuccessful = true;

                Role role = await UnitOfWork.RoleRepository.GetByIdAsync(id);
                if (role == null)
                {
                    result.AddErrorMessage(typeof(Role) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                await UnitOfWork.RoleRepository.DeleteAsync(role);
                await UnitOfWork.SaveAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<RoleViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<RoleViewModel>();
                result.IsSuccessful = true;

                Role role = await UnitOfWork.RoleRepository.GetByIdAsync(id);
                if (role == null)
                {
                    throw new NullReferenceException(typeof(Role) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = role.Adapt<RoleViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<RoleViewModel>>> GetAllAsync()
        {
            try
            {
                var result = new Result<ICollection<RoleViewModel>>();
                result.IsSuccessful = true;

                var roles = await UnitOfWork.RoleRepository.GetAllAsync();

                result.Data = roles.Adapt<ICollection<RoleViewModel>>();

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

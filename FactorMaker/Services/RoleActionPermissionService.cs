using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServiceIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Role;
using ViewModels.RoleActionPermission;

namespace FactorMaker.Services
{
    public class RoleActionPermissionService : BaseServiceWithDatabase, IRoleActionPermissionService
    {
        public RoleActionPermissionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Result<RoleActionPermissionViewModel>> InsertAsync(RoleActionPermissionViewModel viewModel)
        {
            try
            {
                var result = new Result<RoleActionPermissionViewModel>();
                result.IsSuccessful = true;

                var any = await UnitOfWork.RoleActionPermissionRepository
                    .AnyByRoleIdActionPermissionIdAsync(viewModel.RoleId, viewModel.ActionPermissionId);
                if (any)
                {
                    result.AddErrorMessage(typeof(RoleActionPermission) + " " + ErrorMessages.AlreadyExists);
                    result.IsSuccessful = false;

                    return result;
                }

                var role = await UnitOfWork.RoleRepository.GetByIdAsync(viewModel.RoleId);
                if (role == null)
                {
                    result.AddErrorMessage(typeof(Role) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                var actionPermission = await UnitOfWork.ActionPermissionRepository.GetByIdAsync(viewModel.ActionPermissionId);
                if (actionPermission == null)
                {
                    result.AddErrorMessage(typeof(ActionPermission) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                var roleActionPermission = viewModel.Adapt<RoleActionPermission>();

                await UnitOfWork.RoleActionPermissionRepository.InsertAsync(roleActionPermission);
                await UnitOfWork.SaveAsync();

                result.Data = roleActionPermission.Adapt<RoleActionPermissionViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<RoleActionPermissionViewModel>> UpdateAsync(RoleActionPermissionViewModel viewModel)
        {
            try
            {
                var result = new Result<RoleActionPermissionViewModel>();
                result.IsSuccessful = true;

                var roleActionPermission = await UnitOfWork.RoleActionPermissionRepository.GetByIdAsync(viewModel.Id);
                if(roleActionPermission == null)
                {
                    result.AddErrorMessage(typeof(RoleActionPermission) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;

                    return result;
                }

                var role = await UnitOfWork.RoleRepository.GetByIdAsync(viewModel.RoleId);
                if (role == null)
                {
                    result.AddErrorMessage(typeof(Role) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                var actionPermission = await UnitOfWork.ActionPermissionRepository.GetByIdAsync(viewModel.ActionPermissionId);
                if (actionPermission == null)
                {
                    result.AddErrorMessage(typeof(ActionPermission) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                roleActionPermission.RoleId = role.Id;
                roleActionPermission.ActionPermissionId = actionPermission.Id;

                UnitOfWork.RoleActionPermissionRepository.Update(roleActionPermission);
                UnitOfWork.Save();

                result.Data = roleActionPermission.Adapt<RoleActionPermissionViewModel>();
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

                var roleActionPermission = await UnitOfWork.RoleActionPermissionRepository.GetByIdAsync(id);
                if (roleActionPermission == null)
                {
                    result.AddErrorMessage(typeof(Role) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                await UnitOfWork.RoleActionPermissionRepository.DeleteAsync(roleActionPermission);
                await UnitOfWork.SaveAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<RoleActionPermissionViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<RoleActionPermissionViewModel>();
                result.IsSuccessful = true;

                var roleActionPermission = await UnitOfWork.RoleActionPermissionRepository.GetByIdAsync(id);
                if (roleActionPermission == null)
                {
                    result.AddErrorMessage(typeof(RoleActionPermission) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = roleActionPermission.Adapt<RoleActionPermissionViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<RoleActionPermissionViewModel>>> GetAllAsync()
        {
            try
            {
                var result = new Result<ICollection<RoleActionPermissionViewModel>>();
                result.IsSuccessful = true;

                var list = await UnitOfWork.RoleActionPermissionRepository.GetAllAsync();

                result.Data = list.Adapt<ICollection<RoleActionPermissionViewModel>>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

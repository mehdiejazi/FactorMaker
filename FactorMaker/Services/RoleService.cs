using FactorMaker.Services.Base;
using Data;
using Models;
using System;
using System.Threading.Tasks;
using Resources;
using System.Collections.Generic;
using FactorMaker.Services.ServicesIntefaces;
using System.Linq;

namespace FactorMaker.Services
{
    public class RoleService : BaseServiceWithDatabase, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Role Insert(string roleName, string description)
        {
            try
            {
                Role role = new Role
                {
                    RolName = roleName,
                    Description = description,
                };

                UnitOfWork.RoleRepository.Insert(role);
                UnitOfWork.Save();

                return role;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Role> InsertAsync(string roleName, string description)
        {
            try
            {
                Role role = new Role
                {
                    RolName = roleName,
                    Description = description,
                };

                await UnitOfWork.RoleRepository.InsertAsync(role);
                await UnitOfWork.SaveAsync();

                return role;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Role Update(Guid id, string roleName, string description)
        {
            try
            {
                Role role = UnitOfWork.RoleRepository.GetById(id);

                if (role == null)
                    throw new NullReferenceException(typeof(Role) + " " + ErrorMessages.NotFound);

                role.RolName = roleName;
                role.Description = description;

                UnitOfWork.RoleRepository.Update(role);
                UnitOfWork.Save();

                return role;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Role> UpdateAsync(Guid id, string roleName, string description)
        {
            try
            {
                Role role = await UnitOfWork.RoleRepository.GetByIdAsync(id);

                if (role == null)
                    throw new NullReferenceException(typeof(Role) + " " + ErrorMessages.NotFound);

                role.RolName = roleName;
                role.Description = description;

                UnitOfWork.RoleRepository.Update(role);
                UnitOfWork.Save();

                return role;

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
                Role role = UnitOfWork.RoleRepository.GetById(id);

                if (role == null)
                    throw new NullReferenceException(typeof(Role) + " " + ErrorMessages.NotFound);

                UnitOfWork.RoleRepository.Delete(role);
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
                Role role = await UnitOfWork.RoleRepository.GetByIdAsync(id);

                if (role == null)
                    throw new NullReferenceException(typeof(Role) + " " + ErrorMessages.NotFound);

                await UnitOfWork.RoleRepository.DeleteAsync(role);
                await UnitOfWork.SaveAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Role GetById(Guid id)
        {
            try
            {
                Role role = UnitOfWork.RoleRepository.GetById(id);

                if (role == null)
                    throw new NullReferenceException(typeof(Role) + " " + ErrorMessages.NotFound);

                return role;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<Role> GetByIdAsync(Guid id)
        {
            try
            {
                Role role = await UnitOfWork.RoleRepository.GetByIdAsync(id);

                if (role == null)
                    throw new NullReferenceException(typeof(Role) + " " + ErrorMessages.NotFound);

                return role;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ICollection<Role> GetAll()
        {
            try
            {
                var roles = UnitOfWork.RoleRepository.GetAll();

                return roles;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ICollection<Role>> GetAllAsync()
        {
            try
            {
                var roles = await UnitOfWork.RoleRepository.GetAllAsync();

                return roles;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Role AddActionPermission(Guid roleId, Guid actionPermissionId)
        {
            try
            {
                Role role = UnitOfWork.RoleRepository.GetById(roleId);
                if (role == null)
                    throw new NullReferenceException(typeof(Role) + " " + ErrorMessages.NotFound);

                ActionPermission action = UnitOfWork.ActionPermissionRepository.GetById(actionPermissionId);
                if (action == null)
                    throw new NullReferenceException(typeof(ActionPermission) + " " + ErrorMessages.NotFound);

                if (role.RoleActionPermissions
                    .Where(x => x.ActionPermissionId == actionPermissionId)
                    .Count() == 0)
                {

                    role.RoleActionPermissions.Add(new RoleActionPermission()
                    {
                        ActionPermission = action,
                        ActionPermissionId = action.Id,
                        Role = role,
                        RoleId = role.Id,
                    });

                    UnitOfWork.Save();
                }
                return role;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Role> AddActionPermissionAsync(Guid roleId, Guid actionPermissionId)
        {
            try
            {
                Role role = await UnitOfWork.RoleRepository.GetByIdAsync(roleId);
                if (role == null)
                    throw new NullReferenceException(typeof(Role) + " " + ErrorMessages.NotFound);

                ActionPermission action = UnitOfWork.ActionPermissionRepository.GetById(actionPermissionId);
                if (action == null)
                    throw new NullReferenceException(typeof(ActionPermission) + " " + ErrorMessages.NotFound);

                if (role.RoleActionPermissions
                   .Where(x => x.ActionPermissionId == actionPermissionId)
                   .Count() == 0)
                {
                    role.RoleActionPermissions.Add(new RoleActionPermission()
                    {
                        ActionPermission = action,
                        ActionPermissionId = action.Id,
                        Role = role,
                        RoleId = role.Id,
                    });

                    await UnitOfWork.SaveAsync();
                }
                return role;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

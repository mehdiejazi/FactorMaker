using FactorMaker.Services.Base;
using Data;
using Models;
using System;
using System.Threading.Tasks;
using Resources;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Infrastructure;
using FactorMaker.Services.ServicesIntefaces;

namespace FactorMaker.Services
{
    public class ActionPermissionService : BaseServiceWithDatabase, IActionPermissionService
    {
        public ActionPermissionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            SyncActions();
        }

        private void SyncActions()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(BaseApiController).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = System.String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            var actionPermissions =
                UnitOfWork.ActionPermissionRepository.GetAll().ToArray();

            if (actionPermissions.Count() != controlleractionlist.Count())
            {
                foreach (var item in controlleractionlist)
                {
                    if (actionPermissions
                        .Where(a => a.ControllerName == item.Controller
                        && a.ActionName == item.Action).Count() == 0)
                    {
                        ActionPermission ap = new ActionPermission();
                        ap.ControllerName = item.Controller;
                        ap.ActionName = item.Action;
                        ap.Url = "/" + item.Controller.Replace("Controller", "") + "/" + item.Action;

                        UnitOfWork.ActionPermissionRepository.Insert(ap);
                    }
                }
            }

            UnitOfWork.Save();
        }

        public ActionPermission GetById(Guid id)
        {
            try
            {
                ActionPermission actionPermission = UnitOfWork.ActionPermissionRepository.GetById(id);

                if (actionPermission == null)
                    throw new NullReferenceException(typeof(ActionPermission) + " " + ErrorMessages.NotFound);

                return actionPermission;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionPermission> GetByIdAsync(Guid id)
        {
            try
            {
                ActionPermission actionPermission =
                    await UnitOfWork.ActionPermissionRepository.GetByIdAsync(id);

                if (actionPermission == null)
                    throw new NullReferenceException(typeof(ActionPermission) + " " + ErrorMessages.NotFound);

                return actionPermission;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ICollection<ActionPermission> GetAll()
        {
            try
            {
                var actions = UnitOfWork.ActionPermissionRepository.GetAll();

                return actions;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ICollection<ActionPermission>> GetAllAsync()
        {
            try
            {
                var actions = await UnitOfWork.ActionPermissionRepository.GetAllAsync();

                return actions;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

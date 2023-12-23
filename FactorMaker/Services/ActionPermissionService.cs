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
using Common;
using ViewModels.ActionPermission;
using Mapster;

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

        public async Task<Result<ActionPermissionViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<ActionPermissionViewModel>();
                result.IsSuccessful = true;

                var actionPermission = await UnitOfWork.ActionPermissionRepository.GetByIdAsync(id);
                if (actionPermission == null)
                {
                    result.AddErrorMessage(typeof(ActionPermission) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = actionPermission.Adapt<ActionPermissionViewModel>();
                result.IsSuccessful = true;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<ICollection<ActionPermissionViewModel>>> GetAllAsync()
        {
            try
            {
                var result = new Result<ICollection<ActionPermissionViewModel>>();

                var actions = await UnitOfWork.ActionPermissionRepository.GetAllAsync();

                result.Data = actions.Adapt<ICollection<ActionPermissionViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

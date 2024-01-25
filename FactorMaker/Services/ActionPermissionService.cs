using Common;
using Data;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ViewModels.ActionPermission;

namespace FactorMaker.Services
{
    public class ActionPermissionService : BaseServiceWithDatabase, IActionPermissionService
    {
        public ActionPermissionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Task.Run(() => SyncActionsAsync());
        }

        private async Task SyncActionsAsync()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var controlleractionlist = asm.GetTypes()
                .Where(type => typeof(BaseApiController).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Where(m => m.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any()) // فقط اکشن‌های دارای [Authorize] را انتخاب کن
                .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = System.String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                .OrderBy(x => x.Controller)
                .ThenBy(x => x.Action)
                .ToList();

            var actionPermissions = await UnitOfWork.ActionPermissionRepository.GetAllAsync();

            var missingPermissions = controlleractionlist
                .Where(item => actionPermissions.All(a => a.ControllerName != item.Controller || a.ActionName != item.Action))
                .Select(item => new ActionPermission
                {
                    ControllerName = item.Controller,
                    ActionName = item.Action,
                    Url = "/" + item.Controller.Replace("Controller", "") + "/" + item.Action
                });

            if (missingPermissions.Any())
            {
                await UnitOfWork.ActionPermissionRepository.InsertRangeAsync(missingPermissions);
                await UnitOfWork.SaveAsync();
            }
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

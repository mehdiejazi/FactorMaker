using Common;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using System;
using System.Linq;

namespace FactorMaker.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            User user = context.HttpContext.Items["User"] as User;

            if (user == null)
            {
                var responseModel = new Result();

                responseModel.IsSuccessful = false;
                responseModel.AddErrorMessage(Resources.ErrorMessages.UnauthorizedAccess);

                context.Result = new BaseApiController().Result(responseModel);

                return;
            }

            if (user.Role.Name == "Programmer")
            {
                return;
            }

            var path = context.HttpContext.Request.Path.Value;

            var urlIsAthurized = 
                (user.Role.RoleActionPermissions
                .Where(x => x.ActionPermission.Url == path)
                .Any());

            if (urlIsAthurized == false)
            {
                var responseModel = new Result();

                responseModel.IsSuccessful = false;
                responseModel.AddErrorMessage(Resources.ErrorMessages.UnauthorizedActionAccess);

                context.Result = new BaseApiController().Result(responseModel);
                return;
            }
        }
    }
}

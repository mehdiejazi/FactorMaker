using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace FactorMaker.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public RoleType RoleType { get; set; } = RoleType.User;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            User user = context.HttpContext.Items["User"] as User;

            if (user == null)
            {
                var responseModel = new Result();

                responseModel.IsSuccessful = false;
                responseModel.AddErrorMessage(Resources.ErrorMessages.UnauthorizedAccess);

                context.Result = new JsonResult(responseModel);
            }

            if (RoleType == RoleType.Administrator)
            {
                if (user.Role != RoleType.Administrator)
                {
                    var responseModel = new Result();

                    responseModel.IsSuccessful = false;
                    responseModel.AddErrorMessage(Resources.ErrorMessages.UnauthorizedAccess);

                    context.Result = new JsonResult(responseModel);
                }
            }
        }
    }
}

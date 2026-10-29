using Common;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using System;
using System.Linq;

namespace FactorMaker.Infrastructure.Attributes
{
    /// <summary>
    /// Requires a signed-in user (valid JWT) but no specific URL permission.
    /// Honors [AllowAnonymous] on the action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticatedAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<IAllowAnonymous>().Any())
            {
                return;
            }

            if (context.HttpContext.Items["User"] is User)
            {
                return;
            }

            var responseModel = new Result();
            responseModel.IsSuccessful = false;
            responseModel.AddErrorMessage(Resources.ErrorMessages.UnauthorizedAccess);
            context.Result = new BaseApiController().Result(responseModel);
        }
    }
}

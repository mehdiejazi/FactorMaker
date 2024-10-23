using Common;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace FactorMaker.Infrastructure.Attributes
{
    public class ActionValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as BaseApiController;
            if (controller != null)
            {
                if (!controller.ModelState.IsValid)
                {
                    var response = new Result();
                    response.IsSuccessful = false;
                    response.AddRangeErrorMessages(controller.ModelState.GetErrorMessages());

                    context.Result = controller.Result(response);
                    return;
                }
            }
            else
            {
                // Optional: handle the case when the controller is not a BaseApiController
                throw new InvalidOperationException("The controller must inherit from BaseApiController to use ActionValidator.");
            }

        }
    }
}

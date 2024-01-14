using Common;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FactorMaker.Infrastructure.Attributes
{
    public class ActionValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (BaseApiController)context.Controller;
            if (controller.ModelState.IsValid == false)
            {
                var response = new Result();

                response.IsSuccessful = false;
                response.AddRangeErrorMessages(controller.ModelState.GetErrorMessages());

                context.Result = controller.Result(response);

                return;
            }

        }
    }
}

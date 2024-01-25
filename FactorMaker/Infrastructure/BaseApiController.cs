using Common;
using FactorMaker;
using FactorMaker.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure
{
    [ActionValidator]
    [Microsoft.AspNetCore.Mvc.ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [Microsoft.AspNetCore.Mvc.Produces(System.Net.Mime.MediaTypeNames.Application.Json)]
    [Microsoft.AspNetCore.Cors.EnableCors(policyName: Startup.OTHERS_CORS_POLICY)]

    public class BaseApiController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        public BaseApiController() : base()
        {

        }

        [NonAction]
        public ActionResult Result(Result result)
        {
            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [NonAction]
        public ActionResult BadResult(params string[] messages)
        {
            var res = new Result();
            res.AddRangeErrorMessages(messages);

            return Result(res);
        }

    }
}

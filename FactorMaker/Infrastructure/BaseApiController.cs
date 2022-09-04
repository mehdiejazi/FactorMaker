using FactorMaker;

namespace Infrastructure
{
    [Microsoft.AspNetCore.Mvc.ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [Microsoft.AspNetCore.Mvc.Produces(System.Net.Mime.MediaTypeNames.Application.Json)]
    [Microsoft.AspNetCore.Cors.EnableCors(policyName: Startup.OTHERS_CORS_POLICY)]

    public class BaseApiController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        public BaseApiController() : base()
        {
        }
    }
}

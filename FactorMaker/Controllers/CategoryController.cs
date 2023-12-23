using FactorMaker.Services.ServicesIntefaces;
using FactorMaker.Services;
using Infrastructure;

namespace FactorMaker.Controllers
{
    public class CategoryController : BaseApiController
    {
        private CategoryController() : base()
        {

        }
        public CategoryController(IActionPermissionService actionPermissionService)
        {
            ActionPermissionService = actionPermissionService;
        }
        private IActionPermissionService ActionPermissionService { get; }

    }
}

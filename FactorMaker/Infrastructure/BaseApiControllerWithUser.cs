using Models;

namespace Infrastructure
{
    public class BaseApiControllerWithUser : BaseApiController
    {
        public BaseApiControllerWithUser() : base()
        {
        }

        protected new User User
        {
            get
            {
                if (HttpContext != null && HttpContext.Items != null && (User)HttpContext.Items["User"] != null)
                    return (User)HttpContext.Items["User"];
                else
                    return null;
            }

        }
    }
}

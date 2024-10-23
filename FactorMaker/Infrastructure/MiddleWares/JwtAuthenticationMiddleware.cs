using FactorMaker.Infrastructure.ApplicationSettings;
using FactorMaker.Services.ServiceIntefaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace FactorMaker.Infrastructure.MiddleWares
{
    public class JwtAuthenticationMiddleware
    {
        public JwtAuthenticationMiddleware(RequestDelegate next, AuthSettings authSettings)
        {
            Next = next;
            AuthSettings = authSettings;
        }
        protected RequestDelegate Next { get; }
        protected AuthSettings AuthSettings { get; }
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var requsetHeaders = context.Request.Headers["Authorization"];

            string token = requsetHeaders
                        .FirstOrDefault()
                        ?.Split(" ")
                        .Last();

            if (string.IsNullOrWhiteSpace(token) == false && token.Equals("null") == false)
            {
                await JwtUtility.AttachUserToContextByTokenAsync(context: context,
                 userService: userService, token: token, secretKey: AuthSettings.SecretKey);
            }

            await Next(context);
        }
    }

    public static class ApiErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuthenticationMiddleware>();
        }
    }
}

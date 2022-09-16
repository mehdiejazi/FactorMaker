using FactorMaker.Infrastructure.ApplicationSettings;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Reflection;
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
            //Assembly asm = Assembly.GetExecutingAssembly(); //.GetAssembly(typeof(FactorMaker.Program));

            //var controlleractionlist = asm.GetTypes()
            //        .Where(type => typeof(BaseApiController).IsAssignableFrom(type))
            //        .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            //        .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
            //        .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = System.String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
            //        .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            //controlleractionlist = null;

            var requsetHeaders = context.Request.Headers["Authorization"];

            string token = requsetHeaders
                        .FirstOrDefault()
                        ?.Split(" ")
                        .Last();

            if (string.IsNullOrWhiteSpace(token) == false)
            {
                JwtUtility.AttachUserToContextByToken(context: context,
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

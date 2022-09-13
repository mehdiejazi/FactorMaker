using FactorMaker.Infrastructure.ApplicationSettings;
using FactorMaker.Services.ServicesIntefaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorMaker.Infrastructure.MiddleWares
{
    public class JwtAuthenticationMiddleware
    {
        public JwtAuthenticationMiddleware(RequestDelegate next, Main mainSettings)
        {
            Next = next;
            MainSettings = mainSettings;
        }
        protected RequestDelegate Next { get; }
        protected Main MainSettings { get; }
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var requsetHeaders = context.Request.Headers["Authorization"];

            string token = requsetHeaders
                        .FirstOrDefault()
                        ?.Split(" ")
                        .Last();

            if (string.IsNullOrWhiteSpace(token) == false)
            {
                JwtUtility.AttachUserToContextByToken(context: context,
                    userService: userService, token: token, secretKey: MainSettings.SecretKey);
            }

            await Next(context);
        }
    }

    public static class ApiErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder JwtAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuthenticationMiddleware>();
        }
    }
}

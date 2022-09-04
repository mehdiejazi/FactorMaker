using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApiErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Result();

                responseModel.IsSuccessful = false;
                responseModel.AddErrorMessage(error.Message);

                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                };

                var result = JsonConvert.SerializeObject(responseModel, settings);
                await response.WriteAsync(result);
            }
        }
    }

    public static class ApiErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiErrorHandlerMiddleware>();
        }
    }
}

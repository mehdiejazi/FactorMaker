using System.Linq;
using Common;
using Data;
using FactorMaker.Services;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FactorMaker
{
    public class Startup
    {
        public const string ADMIN_CORS_POLICY = "ADMIN_CORS_POLICY";
        public const string OTHERS_CORS_POLICY = "OTHERS_CORS_POLICY";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
            .AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling
             = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            services.AddCors(options =>
            {
                options.AddPolicy(ADMIN_CORS_POLICY,
                    builder =>
                    {
                        builder
                            .WithOrigins("http://localhost:5001")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            //.AllowCredentials()
                            ;
                    });

                options.AddPolicy(OTHERS_CORS_POLICY,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            //.AllowCredentials()
                            ;
                    });
            });

            services.AddSingleton<MyServer>();

            services.AddTransient<Data.IUnitOfWork, Data.UnitOfWork>(sp =>
            {
                Data.Tools.Options options =
                    new Data.Tools.Options
                    {
                        Provider =
                            (Data.Tools.Enums.Provider)
                            System.Convert.ToInt32(Configuration.GetSection(key: "DatabaseProvider").Value),

                        ConnectionString =
                            Configuration.GetSection(key: "ConnectionStrings").GetSection(key: "FactorMakerConnectionString").Value,
                    };
                return new UnitOfWork(options: options);
            });

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IFactorService, FactorService>();
            services.AddScoped<IFactorItemService, FactorItemService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                { Title = "FactorMaker Web Api Core", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseApiErrorHandlerMiddleware();

            app.UseRouting();
            app.UseCors(policyName: OTHERS_CORS_POLICY);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aratime Web Api Core v1");
            });

            //app.UseSqlServer
        }
    }
}

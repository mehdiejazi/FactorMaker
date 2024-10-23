using Common;
using Data;
using FactorMaker.Infrastructure.ApplicationSettings;
using FactorMaker.Infrastructure.MiddleWares;
using FactorMaker.Infrastructure.Validators.Authentication;
using FactorMaker.Services;
using FactorMaker.Services.ServiceIntefaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Linq;

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

            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<LoginRequestViewModelValidator>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(ADMIN_CORS_POLICY,
                    builder =>
                    {
                        builder
                            .WithOrigins("http://localhost:5001")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });

                options.AddPolicy(OTHERS_CORS_POLICY,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            //services.Configure<AuthSettings>
            //    (Configuration.GetSection("AuthSettings"));

            //services.AddSingleton(Configuration.GetSection("AuthSettings").Get<AuthSettings>());
            //service.AddSingleton(configuration.GetSection("WeirdService").Get<WeirdService>();


            AuthSettings _authSettings = new AuthSettings();
            Configuration.GetSection("AuthSettings").Bind(_authSettings);// bind is necessary
            services.AddSingleton<AuthSettings>(_authSettings);

            //  services.AddSingleton<AuthSettings>(Configuration.GetSection("AuthSettings").Get<AuthSettings>());


            services.AddSingleton<MyServer>();

            services.AddTransient<IUnitOfWork, UnitOfWork>(sp =>
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
            services.AddScoped<IActionPermissionService, ActionPermissionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IFactorItemService, FactorItemService>();
            services.AddScoped<IFactorService, FactorService>();
            services.AddScoped<IImageAssetService, ImageAssetService>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoleActionPermissionService, RoleActionPermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IUserService, UserService>();

            services.AddMemoryCache();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { Title = "FactorMaker Web Api Core", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseApiErrorHandlerMiddleware();
            app.UseJwtAuthenticationMiddleware();

            app.UseRouting();
            app.UseCors(policyName: OTHERS_CORS_POLICY);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
              Path.Combine(env.ContentRootPath, "uploads")),
                RequestPath = "/uploads"
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FactorMaker Web Api Core v1");
            });



            //app.UseSqlServer
        }
    }
}

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Demo.DAL;
using Microsoft.EntityFrameworkCore;
using Demo.Business.InfraStructure;
using NetCore.AutoRegisterDi;
using Demo.Common;
using Demo.Common.Logger;
using Demo.API.Utils;
using Demo.APIModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Demo.API
{
    public class Startup
    {
        readonly string allowedOrigins = "_allowedOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddCorsUrls(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver
                    = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });

            RegisterDependencies(services);
            AddJWTTokenAuthentication(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP reques pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
               // app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                //moved for PROD
                app.UseHttpsRedirection();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors(allowedOrigins);
            app.UseAuthentication();

            //set API Route
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            
           

        }
        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Demo.Business"))
            .Where(x => x.Name.EndsWith("Service"))
            .AsPublicImplementedInterfaces();
        }
        private void AddCorsUrls(IServiceCollection services)
        {
             services.AddCors(options =>
            {
                options.AddPolicy(allowedOrigins,
                builder =>
                {
                    builder.WithOrigins(ApplicationManagement.ValidDomains).AllowAnyHeader()
                                        .AllowAnyMethod();;
                });
            });
        }
        private void AddJWTTokenAuthentication(IServiceCollection services){
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsModel>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettingsModel>();
            var key = Encoding.ASCII.GetBytes(appSettings.TokenOptions.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.Events = new JwtBearerEvents{
                    OnAuthenticationFailed = context =>
                    {
                       if(context.Exception is SecurityTokenExpiredException)
                       {
                          context.Response.OnStarting(async () =>
                          {
                            await context.Response.WriteAsync("TOKEN_EXPIRED");
                          });
                       }
                         return Task.CompletedTask;
 
                    },
                    OnTokenValidated = context =>
                    {
                        SetUserInfo(context);
                        return Task.CompletedTask;
                    }
                };
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime= true,
                    ValidIssuer = appSettings.TokenOptions.Issuer,
                    ValidAudience = appSettings.TokenOptions.Audience
                };
            });
        }
        private void SetUserInfo(TokenValidatedContext context)
        {
            var claims = context.Principal.Claims;
            UserStateManagement.UserId =Convert.ToInt32(context.Principal.Identity.Name);
            var isSuperAdmin = claims.Where(x => x.Type == "IsSuperAdmin").FirstOrDefault();
            if(isSuperAdmin != null)
                UserStateManagement.IsSuperAdmin = Convert.ToBoolean(isSuperAdmin.Value);
        }
      
    }
}

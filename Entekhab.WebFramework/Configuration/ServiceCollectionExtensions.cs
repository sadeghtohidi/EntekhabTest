using Common;
using Common.Exceptions;
using Common.Utilities;
using Data;
using Data.Repositories;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Entekhab.Data.Contracts;
using Entekhab.Data.Database;
using Entekhab.Data.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace WebFramework.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseSqlServer(configuration.GetConnectionString("SqlServer"));
                    //Tips
                    //.ConfigureWarnings(warning => warning.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
        }

        //for dapper
        public static void AddCustomDapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDatabaseConnectionFactory>(e =>
            {
                return new SqlConnectionFactory(configuration.GetConnectionString("SqlServer"));
            });
        }


        //سرویس های لازم برای مینیمال کردن  ادد ام وی سی
        //public static void AddMinimalMvc(this IServiceCollection services)
        //{
        //    //https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
        //    services.AddMvcCore(options =>
        //    {
        //        options.Filters.Add(new AuthorizeFilter());

        //    })
        //    .AddApiExplorer()
        //    .AddAuthorization()
        //    .AddFormatterMappings()
        //    .AddDataAnnotations()
        //    //.AddJsonFormatters()
        //    .AddCors()
        //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        //}

        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public static void AddElmah(this IServiceCollection services, IConfiguration configuration, SiteSettings siteSetting)
        {
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.Path = siteSetting.ElmahPath;
                options.ConnectionString = configuration.GetConnectionString("SqlServer");
                //options.CheckPermissionAction = httpContext =>
                //{
                //    return httpContext.User.Identity.IsAuthenticated;
                //};
            });
        }

      
    }
}

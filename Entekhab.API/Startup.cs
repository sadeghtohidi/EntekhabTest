using Autofac;
using Microsoft.AspNetCore.Identity;
using WebFramework.Configuration;
using System.Configuration;
using Microsoft.OpenApi.Models;
using Autofac.Extensions.DependencyInjection;
using Entekhab.Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using ElmahCore.Mvc;
using AutoMapper;
using WebFramework.CustomMapping;
using Common;
using ElmahCore.Sql;
using Microsoft.Extensions.Configuration;

namespace Entekhab.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

            //AutoMapperConfiguration.InitializeAutoMapper();


        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {


            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                                _configuration.GetConnectionString("SqlServer"),
                                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // add Auto Mapper
            services.InitializeAutoMapper();

            services.AddAutofac();
            // add Elmah Log          
            services.AddElmah<SqlErrorLog>(options =>
            {
                
                options.ConnectionString = _configuration.GetConnectionString("SqlServer");
                //options.CheckPermissionAction = httpContext =>
                //{
                //    return httpContext.User.Identity.IsAuthenticated;
                //};
            });

            //Autofac
            return services.BuildAutofacServiceProvider();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , ApplicationDbContext dataContext)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rasad.Api v1");
            });

            dataContext.Database.Migrate();

            // Elmah
            app.UseElmah();

            //for errors
            app.UseHttpsRedirection();

            //for https
            app.UseHsts();

            app.UseRouting();


            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }
            );
        }
    }
}

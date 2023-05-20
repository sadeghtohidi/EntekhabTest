using AdminSalary.DataLayer.Repositories.Dapper;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Common;
using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Entekhab.Data.Contracts;
using Entekhab.Data.Database;
using Entekhab.Data.Repositories;
using Entekhab.Domain.Common;
using Entekhab.Services.Common.AutoFac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static Dapper.SqlMapper;

namespace WebFramework.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        public static void AutofacConfig(this ContainerBuilder builder)
        {
            var commonAssembly = typeof(SiteSettings).Assembly;
            var entitiesAssembly = typeof(IEntity).Assembly;
            var dataAssembly = typeof(ApplicationDbContext).Assembly;
            var agentAssemblyService = typeof(Entekhab.Services.Common.AutoFac.IScopedDependency).Assembly;
            //var agentAssemblyData = typeof(Entekhab.Data.Common.AutoFac.IScopedDependency).Assembly;

            var assembly = typeof(StartupBase).Assembly;
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            // Get List Assemply
            var listInterface = Assembly.Load(agentAssemblyService.ToString())
                                    .GetTypes()
                                    .Where(w => w.IsInterface).ToList();


            //builder.RegisterType<TestModelService>().As<ITestModelService>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DapperRepositoryTEntity<>)).As(typeof(IDapperRepositoryTEntity<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(agentAssemblyService)
                            .AssignableTo<IScopedDependency>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(agentAssemblyService)
                            .AssignableTo<ITransientDependency>()
                            .AsImplementedInterfaces()
                            .InstancePerDependency();

            builder.RegisterAssemblyTypes(agentAssemblyService)
                            .AssignableTo<ISingletonDependency>()
                            .AsImplementedInterfaces()
                            .SingleInstance();




    
        }


        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            //RegisterType > As > Liftetime
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();


            var commonAssembly = typeof(SiteSettings).Assembly;
            var entitiesAssembly = typeof(IEntity).Assembly;
            var dataAssembly = typeof(ApplicationDbContext).Assembly;
            //var servicesAssembly = typeof(JwtService).Assembly;

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        public static IServiceProvider BuildAutofacServiceProvider(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            //Register Services to Autofac ContainerBuilder
            containerBuilder.AutofacConfig();

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}

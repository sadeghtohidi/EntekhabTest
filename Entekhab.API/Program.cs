using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Entekhab;


namespace Entekhab.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
          
        .ConfigureLogging(options => options.ClearProviders())
        .UseStartup<Startup>();
    }
}


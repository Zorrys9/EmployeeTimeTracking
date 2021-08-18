using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EmployeeTimeTracking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                 WebHost.CreateDefaultBuilder(args)
                 .UseSerilog(((context, config) =>
                 {
                     config.ReadFrom.Configuration(context.Configuration);
                 }))
                 .UseStartup<Startup>();
    }
}

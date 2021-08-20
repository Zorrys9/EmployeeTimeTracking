using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using EmployeeTimeTracking._Logic.Logics;
using EmployeeTimeTracking._Logic.Logics.Implementations;
using EmployeeTimeTracking._Services.Services;
using EmployeeTimeTracking._Services.Services.Implementations;
using EmployeeTimeTracking.Data.Repository;
using EmployeeTimeTracking.Data.Repository.Implementations;
using EmployeeTimeTracking.Middlewares;
using EmployeeTimeTracking.Modules;
using EmployeeTimeTracking.Services.Services;
using EmployeeTimeTracking.Services.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;

namespace EmployeeTimeTracking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                });
            });

            var connectionString = Configuration.GetConnectionString("Default");
            var builder = new ContainerBuilder();
            ILogger logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            builder.Properties.Add("ConnectionString", connectionString);
            builder.Populate(services);
            builder.RegisterModule<AutoFacModule>();
           

            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

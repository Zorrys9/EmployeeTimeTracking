using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmployeeTimeTracking._Logic.Logics;
using EmployeeTimeTracking._Logic.Logics.Implementations;
using EmployeeTimeTracking._Services.Services;
using EmployeeTimeTracking._Services.Services.Implementations;
using EmployeeTimeTracking.Data.Repository;
using EmployeeTimeTracking.Data.Repository.Implementations;
using EmployeeTimeTracking.Modules;
using EmployeeTimeTracking.Services.Services;
using EmployeeTimeTracking.Services.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace EmployeeTimeTracking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
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

            builder.Populate(services);
            builder.RegisterModule<AutoFacModule>();

            builder.RegisterType<EmployeeRepository>()
                .As<IEmployeeRepository>()
                .InstancePerLifetimeScope()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<ReportRepository>()
                .As<IReportRepository>()
                .InstancePerLifetimeScope()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<EmployeeReportRepository>()
                .As<IEmployeeReportRepository>()
                .InstancePerLifetimeScope()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<SummaryReportRepository>()
                .As<ISummaryReportRepository>()
                .InstancePerLifetimeScope()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<EmployeeService>()
                .As<IEmployeeService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ReportService>()
                .As<IReportService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmployeeReportService>()
                .As<IEmployeeReportService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FileService>()
                .As<IFileService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TrackingLogic>()
                .As<ITrackingLogic>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountLogic>()
                .As<IAccountLogic>()
                .InstancePerLifetimeScope();

            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
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

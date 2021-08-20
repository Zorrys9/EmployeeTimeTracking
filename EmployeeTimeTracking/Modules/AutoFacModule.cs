using Autofac;
using AutoMapper;
using EmployeeTimeTracking._Logic.Logics;
using EmployeeTimeTracking._Logic.Logics.Implementations;
using EmployeeTimeTracking._Services.Services;
using EmployeeTimeTracking._Services.Services.Implementations;
using EmployeeTimeTracking.Data.Repository;
using EmployeeTimeTracking.Data.Repository.Implementations;
using EmployeeTimeTracking.MapperProfiles;
using EmployeeTimeTracking.Services.Services;
using EmployeeTimeTracking.Services.Services.Implementations;
using Microsoft.Extensions.Configuration;

namespace EmployeeTimeTracking.Modules
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            string connectionString = builder.Properties["ConnectionString"].ToString();
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
                cfg.AddProfile<ReportProfile>();
                cfg.AddProfile<EmployeeReportProfile>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(component =>
            {
                var context = component.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

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

            builder.RegisterType<SearchReportsRepository>()
                .As<ISearchReportsRepository>()
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
        }
    }
}

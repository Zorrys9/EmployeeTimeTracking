using Autofac;
using AutoMapper;
using EmployeeTimeTracking.MapperProfiles;

namespace EmployeeTimeTracking.Modules
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
        }



    }
}

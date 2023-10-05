using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace VebTech.Infrastructure.Database;

public class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<DatabaseContext>()
            .WithParameter("option", DbContextOptionsFactory.Get())
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces();
    }
}
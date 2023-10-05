
using System.Reflection;
using Autofac;
using MediatR;
using Module = Autofac.Module;

namespace VebTech.Application.Requests;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AsClosedTypesOf(typeof(IRequestHandler<,>));
        
        builder
            .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AsClosedTypesOf(typeof(IPipelineBehavior<,>));
    }
}
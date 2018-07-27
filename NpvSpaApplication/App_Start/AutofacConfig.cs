using Autofac;
using Autofac.Integration.WebApi;
using NpvSpaApplication.Service;
using System.Reflection;

namespace NpvSpaApplication
{
    internal static class AutofacConfig
    {
        internal static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ComputeService>().As<IComputeService>().SingleInstance();  

            return builder.Build();
        }
    }
}

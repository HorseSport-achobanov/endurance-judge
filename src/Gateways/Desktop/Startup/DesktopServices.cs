using EnduranceJudge.Application;
using EnduranceJudge.Core;
using EnduranceJudge.Gateways.Desktop.Core.Objects;
using EnduranceJudge.Core.Services;
using EnduranceJudge.Domain;
using EnduranceJudge.Gateways.Persistence;
using EnduranceJudge.Gateways.Persistence.Startup;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using System.Linq;
using System.Reflection;

namespace EnduranceJudge.Gateways.Desktop.Startup
{
    public static class DesktopServices
    {
        public static IContainerRegistry AddServices(this IContainerRegistry container)
        {
            new ServiceCollection()
                .AddApplicationServices()
                .AdaptToDesktop(container);

            return container;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assemblies = CoreConstants.Assemblies
                .Concat(DomainConstants.Assemblies)
                .Concat(ApplicationConstants.Assemblies)
                .Concat(PersistenceConstants.Assemblies)
                .Concat(DesktopConstants.Assemblies)
                .ToArray();

            return services
                .AddCore(assemblies)
                .AddPersistence(assemblies)
                .AddInitializers(assemblies);
        }

        private static IServiceCollection AddInitializers(this IServiceCollection services, Assembly[] assemblies)
            => services
                .Scan(scan => scan
                    .FromAssemblies(assemblies)
                    .AddClasses(classes =>
                        classes.AssignableTo<IInitializer>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime());

        private static IServiceCollection AdaptToDesktop(
            this IServiceCollection services,
            IContainerRegistry desktopContainer)
        {
            var adapter = new DesktopContainerAdapter(desktopContainer);
            foreach (var serviceDescriptor in services)
            {
                adapter.Register(serviceDescriptor);
            }

            return services;
        }
    }
}

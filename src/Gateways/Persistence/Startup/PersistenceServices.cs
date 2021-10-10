using EnduranceJudge.Gateways.Persistence.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace EnduranceJudge.Gateways.Persistence.Startup
{
    public static class PersistenceServices
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());

            return services;
        }
    }
}

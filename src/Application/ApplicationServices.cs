using EnduranceJudge.Application.Core;
using EnduranceJudge.Application.Models;
using EnduranceJudge.Application.Services;
using EnduranceJudge.Application.State;
using EnduranceJudge.Domain.AggregateRoots.Manager;
using EnduranceJudge.Domain.State;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EnduranceJudge.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies)
    {
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueries<>)))
            .AsSelfWithInterfaces()
            .WithTransientLifetime());
        
        // TODO: get that working
        // services.AddHttpClient();
        // var kur = services.FirstOrDefault(x => x.ServiceType == typeof(IServiceScopeFactory));

        services.AddState();
        
        return services;
    }

    private static IServiceCollection AddState(this IServiceCollection services)
    {
        services.AddSingleton<IStateSetter, StateModel>();
        services.AddSingleton<IState>(x => x.GetRequiredService<IStateSetter>());
        services.AddTransient<IStateContext, StateContext>();
        services.AddWitnessAwareContext();
        return services;
    }
    
    private static IServiceCollection AddWitnessAwareContext(this IServiceCollection services)
    {
        services.AddSingleton<IWitnessPollingService, WitnessPollingService>();
        services.AddTransient<IWitnessAwareContext, WitnessAwareContext>();
        
        var current = services.FirstOrDefault(x => x.ServiceType == typeof(ManagerRoot));
        if (current == null)
        {
            throw new Exception(
                $"Descriptor for aggregate root '{nameof(ManagerRoot)}' is not found. " +
                $"It has to be registered before calling '{nameof(AddApplication)}' in order to configure" +
                $"{nameof(IWitnessAwareContext)}.");
        }
        // TODO: Remove this
        var newDescriptor = new ServiceDescriptor(
            typeof(ManagerRoot),
            x => new ManagerRoot(x.GetRequiredService<IWitnessAwareContext>()),
            ServiceLifetime.Singleton);

        services.Replace(newDescriptor);
        return services;
    }
}

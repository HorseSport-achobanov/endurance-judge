using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using System;

namespace EnduranceJudge.Gateways.Desktop.Core.Objects
{
    internal class DesktopContainerAdapter
    {
        private readonly IContainerRegistry container;

        public DesktopContainerAdapter(IContainerRegistry container)
        {
            this.container = container;
        }

        public void Register(ServiceDescriptor service)
        {
            if (service.Lifetime is ServiceLifetime.Transient or ServiceLifetime.Scoped)
            {
                this.RegisterTransient(service);
            }
            else
            {
                this.RegisterSingleton(service);
            }
        }

        private void RegisterSingleton(ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                this.container.RegisterSingleton(descriptor.ServiceType, () => descriptor.ImplementationInstance);
            }
            else if (descriptor.ImplementationFactory != null)
            {
                var dryIotFactory = this.ToDryIotFactory(descriptor.ImplementationFactory);
                this.container.RegisterSingleton(descriptor.ServiceType, dryIotFactory);
            }
            else
            {
                this.container.RegisterSingleton(descriptor.ServiceType, descriptor.ImplementationType);
            }
        }

        private void RegisterTransient(ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                this.container.Register(descriptor.ServiceType, () => descriptor.ImplementationInstance);
            }
            else if (descriptor.ImplementationFactory != null)
            {
                var dryIotFactory = this.ToDryIotFactory(descriptor.ImplementationFactory);
                this.container.Register(descriptor.ServiceType, dryIotFactory);
            }
            else
            {
                this.container.Register(descriptor.ServiceType, descriptor.ImplementationType);
            }
        }

        private Func<IContainerProvider, object> ToDryIotFactory(Func<IServiceProvider, object> factory)
            => container =>
            {
                var provider = container.Resolve<IServiceProvider>();
                return factory(provider);
            };
    }
}

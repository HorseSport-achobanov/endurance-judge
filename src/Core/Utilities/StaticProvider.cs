using Microsoft.Extensions.DependencyInjection;
using System;

namespace EnduranceJudge.Core.Utilities
{
    public static class StaticProvider
    {
        private static IServiceProvider? provider;

        public static void Set(IServiceProvider provider)
        {
            StaticProvider.provider = provider;
        }

        public static void Initialize(IServiceProvider provider)
        {
            if (StaticProvider.provider != null)
            {
                throw new InvalidOperationException("StaticProvider is already initialized");
            }
            StaticProvider.provider = provider;

        }

        public static T GetService<T>()
        {
            if (StaticProvider.provider == null)
            {
                throw new InvalidOperationException("StaticProvider is not initialized");
            }

            return provider.GetService<T>()!;
        }
    }
}

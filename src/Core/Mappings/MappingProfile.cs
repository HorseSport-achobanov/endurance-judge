using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using System.Collections.Generic;
using EnduranceJudge.Core.Extensions;
using EnduranceJudge.Core.Utilities;

namespace EnduranceJudge.Core.Mappings
{
    public abstract class MappingProfile : Profile
    {
        private static readonly Type ConventionalMapType = typeof(IConventionalMap);
        private static readonly Type MapFromType = typeof(IMapFrom<>);
        private static readonly Type MapToType = typeof(IMapTo<>);
        private static readonly Type MapType = typeof(IMap<>);
        private static readonly Type CustomMapConfigurationType = typeof(ICustomMapConfiguration);

        protected MappingProfile()
        {
            this.AddConventionalMaps(ReflectionUtilities.GetInstanceTypes(this.Assemblies));
            this.AddCustomMaps();
        }

        protected abstract Assembly[] Assemblies { get; }

        protected void AddConventionalMaps(IEnumerable<Type> instanceTypes)
        {
            var types = instanceTypes
                .Where(x => ConventionalMapType.IsAssignableFrom(x))
                .ToList();

            foreach (var type in types)
            {
                var mapFrom = GetMappingModels(type, MapFromType);
                var mapTo = GetMappingModels(type, MapToType);
                var map = GetMappingModels(type, MapType);

                mapFrom.ForEach(x => this.CreateMap(x, type));
                mapTo.ForEach(x => this.CreateMap(type, x));
                map.ForEach(x =>
                {
                    this.CreateMap(type, x);
                    this.CreateMap(x, type);
                });

                this.CreateMap(type, type);
            }
        }

        private void AddCustomMaps()
        {
            var configurations = ReflectionUtilities
                .GetInstanceTypes(this.Assemblies)
                .Where(CustomMapConfigurationType.IsAssignableFrom)
                .Select(Activator.CreateInstance)
                .Cast<ICustomMapConfiguration>()
                .ToList();

            foreach (var configuration in configurations)
            {
                configuration.AddFromMaps(this);
                configuration.AddToMaps(this);
            }
        }

        protected static IEnumerable<Type> GetMappingModels(Type source, Type mappingType)
            => source
                .GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == mappingType)
                .Select(i => i.GetGenericArguments().First());
    }
}

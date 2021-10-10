using System.Reflection;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Utilities;
using EnduranceJudge.Domain.Core.Models;
using System;
using System.Linq;

namespace EnduranceJudge.Domain.Core
{
    public class DomainMappingProfile : MappingProfile
    {
        private static readonly Type DomainObjectType = typeof(IDomainObject);

        public DomainMappingProfile()
        {
            this.CreateDomainMaps();
        }

        protected override Assembly[] Assemblies => DomainConstants.Assemblies;

        private void CreateDomainMaps()
        {
            var types = ReflectionUtilities
                .GetInstanceTypes(this.Assemblies)
                .Where(t => DomainObjectType.IsAssignableFrom(t));
            foreach (var type in types)
            {
                this.CreateMap(type, type);
            }
        }
    }
}

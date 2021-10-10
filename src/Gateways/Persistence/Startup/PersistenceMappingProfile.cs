using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Gateways.Persistence.Contracts;
using System.Reflection;

namespace EnduranceJudge.Gateways.Persistence.Startup
{
    public class PersistenceMappingProfile : MappingProfile
    {
        public PersistenceMappingProfile()
        {
            this.CreateMap<IState, State>();
        }

        protected override Assembly[] Assemblies { get; } = PersistenceConstants.Assemblies;
    }
}

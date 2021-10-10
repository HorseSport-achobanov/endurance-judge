using System.Reflection;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Utilities;

namespace EnduranceJudge.Application.Core
{
    public class ApplicationMappingProfile : MappingProfile
    {
        protected override Assembly[] Assemblies => ApplicationConstants.Assemblies;
    }
}

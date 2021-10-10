using AutoMapper;
using EnduranceJudge.Core.Mappings;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Personnel.Configuration
{
    public class PersonnelMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<Domain.State.Personnels.Personnel, PersonnelViewModel>()
                .ForMember(x => x.Role, opt => opt.Ignore())
                .ForMember(x => x.RoleId, opt => opt.MapFrom(y => (int)y.Role));
        }

        public void AddToMaps(IProfileExpression profile)
        {
        }
    }
}

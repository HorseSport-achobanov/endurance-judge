using AutoMapper;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Mappings.Converters;
using EnduranceJudge.Domain.State.Horses;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Horses.Configuration
{
    public class HorseViewModelMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<Horse, HorseViewModel>()
                .ForMember(
                    x => x.IsStallionValue,
                    opt => opt.ConvertUsing(new BoolToIntConverter(), x => x.IsStallion));
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<HorseViewModel, Horse>();
        }
    }
}

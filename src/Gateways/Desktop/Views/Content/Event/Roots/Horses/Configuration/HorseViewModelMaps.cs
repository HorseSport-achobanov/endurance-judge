using AutoMapper;
using EnduranceJudge.Application.Events.Models;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Mappings.Converters;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Horses.Configuration
{
    public class HorseViewModelMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<HorseRootModel, HorseViewModel>()
                .ForMember(
                    x => x.IsStallionValue,
                    opt => opt.ConvertUsing(new BoolToIntConverter(), x => x.IsStallion));
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<HorseViewModel, HorseRootModel>();
        }
    }
}

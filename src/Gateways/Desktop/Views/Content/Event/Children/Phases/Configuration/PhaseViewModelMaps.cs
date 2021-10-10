using AutoMapper;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Mappings.Converters;
using EnduranceJudge.Domain.State.Phases;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Phases.Configuration
{
    public class PhaseViewModelMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<Phase, PhaseViewModel>()
                .ForMember(x => x.IsFinalValue, opt => opt.ConvertUsing(new BoolToIntConverter(), y => y.IsFinal));
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<PhaseViewModel, Phase>();
        }
    }
}

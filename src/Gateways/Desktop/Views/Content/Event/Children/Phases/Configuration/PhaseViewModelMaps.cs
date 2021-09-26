using AutoMapper;
using EnduranceJudge.Application.Events.Common;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Mappings.Converters;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Phases.Configuration
{
    public class PhaseViewModelMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<PhaseDependantModel, PhaseViewModel>()
                .ForMember(x => x.IsFinalValue, opt => opt.ConvertUsing(new BoolToIntConverter(), y => y.IsFinal));
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<PhaseViewModel, PhaseDependantModel>();
        }
    }
}

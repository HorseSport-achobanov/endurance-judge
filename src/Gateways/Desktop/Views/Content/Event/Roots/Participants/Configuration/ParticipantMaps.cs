using AutoMapper;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.SimpleListItem;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Participants.Configuration
{
    public class ParticipantMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<Participant, ListItemModel>();
            profile.CreateMap<Athlete, SimpleListItemViewModel>();
            profile.CreateMap<Horse, SimpleListItemViewModel>();
        }

        public void AddToMaps(IProfileExpression profile)
        {
        }
    }
}

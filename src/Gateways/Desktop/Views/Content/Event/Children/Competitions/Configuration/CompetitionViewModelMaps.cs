using AutoMapper;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Core.Extensions;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.State.Competitions;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Competitions.Configuration
{
    public class CompetitionViewModelMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<CompetitionViewModel, Competition>()
                .MapMember(x => x.Type, y => (CompetitionType)y.TypeValue);
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<Competition, CompetitionViewModel>()
                .MapMember(x => x.TypeValue, y => (int)y.Type);
        }
    }
}

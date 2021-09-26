using AutoMapper;
using EnduranceJudge.Application.Events.Common;
using EnduranceJudge.Core.Extensions;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Enums;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Competitions.Configuration
{
    public class CompetitionViewModelMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<CompetitionViewModel, CompetitionDependantModel>()
                .MapMember(x => x.Type, y => (CompetitionType)y.TypeValue);
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<CompetitionDependantModel, CompetitionViewModel>()
                .MapMember(x => x.TypeValue, y => (int)y.Type);
        }
    }
}

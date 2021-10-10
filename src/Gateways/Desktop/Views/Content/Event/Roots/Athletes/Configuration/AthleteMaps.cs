using AutoMapper;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Core.Extensions;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.State.Athletes;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Athletes.Configuration
{
    public class AthleteMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<Athlete, AthleteViewModel>()
                .MapMember(x => x.CategoryId, y => (int)y.Category);
            profile.CreateMap<Athlete, ListItemModel>();
        }

        public void AddToMaps(IProfileExpression profile)
        {
        }
    }
}

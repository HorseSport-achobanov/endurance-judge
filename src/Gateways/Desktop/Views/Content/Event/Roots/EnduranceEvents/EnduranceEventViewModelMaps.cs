using AutoMapper;
using EnduranceJudge.Application.Aggregates.Configurations;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.State.Countries;
using EnduranceJudge.Domain.State.EnduranceEvents;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Personnel;
using System.Collections.ObjectModel;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.EnduranceEvents
{
    public class EnduranceEventViewModelMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<EnduranceEvent, EnduranceEventViewModel>()
                .AfterMap((s, d) =>
                {
                    var personnel = PersonnelAggregator
                        .Aggregate(s)
                        .MapEnumerable<PersonnelViewModel>();
                    d.Personnel.AddRange(personnel);
                });
            profile.CreateMap<Country, ListItemModel>();

        }

        public void AddToMaps(IProfileExpression profile)
        {
        }
    }
}

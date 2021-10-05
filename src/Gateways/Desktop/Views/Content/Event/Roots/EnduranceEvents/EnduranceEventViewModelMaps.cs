﻿using AutoMapper;
using EnduranceJudge.Core.Extensions;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.State.EnduranceEvents;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.EnduranceEvents
{
    public class EnduranceEventViewModelMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<EnduranceEventViewModel, EnduranceEvent>()
                .MapMember(d => d.CountryIsoCode, s => s.SelectedCountryIsoCode);
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<EnduranceEvent, EnduranceEventViewModel>()
                .MapMember(d => d.SelectedCountryIsoCode, s => s.CountryIsoCode);
        }
    }
}

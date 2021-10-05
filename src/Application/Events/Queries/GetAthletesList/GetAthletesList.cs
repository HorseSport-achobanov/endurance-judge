﻿using EnduranceJudge.Application.Core.Handlers;
using EnduranceJudge.Application.Events.Common;
using EnduranceJudge.Domain.State.Athletes;
using MediatR;
using System.Collections.Generic;

namespace EnduranceJudge.Application.Events.Queries.GetAthletesList
{
    public class GetAthletesList : IRequest<IEnumerable<ListItemModel>>
    {
        public class GetAthletesHandler : GetAllHandler<GetAthletesList, ListItemModel, Athlete>
        {
            public GetAthletesHandler() : base()
            {
            }
        }
    }
}

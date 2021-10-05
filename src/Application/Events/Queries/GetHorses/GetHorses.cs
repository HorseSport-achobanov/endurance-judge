﻿using EnduranceJudge.Application.Core.Handlers;
using EnduranceJudge.Domain.State.Horses;
using MediatR;
using System.Collections.Generic;

namespace EnduranceJudge.Application.Events.Queries.GetHorses
{
    public class GetHorses : IRequest<IEnumerable<HorseModel>>
    {
        public class GetHorsesHandler : GetAllHandler<GetHorses, HorseModel, Horse>
        {
            public GetHorsesHandler() : base()
            {
            }
        }
    }
}

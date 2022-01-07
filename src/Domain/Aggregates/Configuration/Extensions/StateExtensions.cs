﻿using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.EnduranceEvents;
using static EnduranceJudge.Localization.Translations.Messages;

namespace EnduranceJudge.Domain.Aggregates.Configuration.Extensions;

public static class StateExtensions
{
    public static void ValidateThatEventHasNotStarted(this IState state)
    {
        if (state.Event.HasStarted)
        {
            throw new EnduranceEventException
            {
                DomainMessage = CHANGE_NOT_ALLOWED_WHEN_EVENT_HAS_STARTED,
            };
        }
    }
}
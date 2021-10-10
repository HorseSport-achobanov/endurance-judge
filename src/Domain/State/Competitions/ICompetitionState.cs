using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Enums;
using System;

namespace EnduranceJudge.Domain.State.Competitions
{
    public interface ICompetitionState : IDomainModelState
    {
        CompetitionType Type { get; }
        string Name { get; }
        DateTime StartTime { get; }
    }
}

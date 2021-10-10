using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.ParticipationsInPhases
{
    public class RankingParticipationInPhaseObjectException : DomainObjectException
    {
        protected override string Entity { get; } = $"Ranking {nameof(ParticipationInPhase)}";
    }
}

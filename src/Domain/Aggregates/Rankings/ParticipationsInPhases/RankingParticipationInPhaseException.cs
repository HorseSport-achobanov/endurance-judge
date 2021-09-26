using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.ParticipationsInPhases
{
    public class RankingParticipationInPhaseException : DomainException
    {
        protected override string Entity { get; } = $"Ranking {nameof(ParticipationInPhase)}";
    }
}

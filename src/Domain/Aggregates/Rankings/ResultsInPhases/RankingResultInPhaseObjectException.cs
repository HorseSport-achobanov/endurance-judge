using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.ResultsInPhases
{
    public class RankingResultInPhaseObjectException : DomainObjectException
    {
        protected override string Entity { get; } = $"Ranking: {nameof(ResultInPhase)}";
    }
}

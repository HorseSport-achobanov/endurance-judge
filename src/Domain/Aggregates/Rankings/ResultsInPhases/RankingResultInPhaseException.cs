using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.ResultsInPhases
{
    public class RankingResultInPhaseException : DomainException
    {
        protected override string Entity { get; } = $"Ranking: {nameof(ResultInPhase)}";
    }
}

using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Competitions
{
    public class RankingCompetitionException : DomainException
    {
        protected override string Entity { get; } = $"Ranking {nameof(Competition)}";
    }
}

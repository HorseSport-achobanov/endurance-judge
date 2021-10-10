using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Competitions
{
    public class RankingCompetitionObjectException : DomainObjectException
    {
        protected override string Entity { get; } = $"Ranking {nameof(Competition)}";
    }
}

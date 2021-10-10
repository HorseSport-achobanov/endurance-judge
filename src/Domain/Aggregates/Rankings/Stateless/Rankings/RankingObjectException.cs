using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Rankings
{
    public class RankingObjectException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(Ranking);
    }
}

using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Rankings
{
    public class RankingException : DomainException
    {
        protected override string Entity { get; } = nameof(Ranking);
    }
}

using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Participations
{
    public class RankingParticipationException : DomainException
    {
        protected override string Entity { get; } = $"Ranking {nameof(Participation)}";
    }
}

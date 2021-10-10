using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Manager.ParticipationsInCompetitions
{
    public class ParticipationInCompetitionObjectException : DomainObjectException
    {
        protected override string Entity { get; } = $"Manager {nameof(ParticipationInCompetition)}";
    }
}

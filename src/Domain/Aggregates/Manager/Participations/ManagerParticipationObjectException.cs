using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Manager.Participations
{
    public class ManagerParticipationObjectException : DomainObjectException
    {
        protected override string Entity { get; } = $"Manager {nameof(Participation)}";
    }
}

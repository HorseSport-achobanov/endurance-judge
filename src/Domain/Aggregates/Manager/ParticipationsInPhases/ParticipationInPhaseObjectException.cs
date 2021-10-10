using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Manager.ParticipationsInPhases
{
    public class ParticipationInPhaseObjectException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(PhaseEntryManager);
    }
}

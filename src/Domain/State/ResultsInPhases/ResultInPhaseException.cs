using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.State.ResultsInPhases
{
    public class ManagerResultInPhaseObjectException : DomainObjectException
    {
        protected override string Entity { get; } = $"Manager {nameof(ResultInPhase)}";
    }
}

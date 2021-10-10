using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.State.PhaseEntries
{
    public class PhaseEntryException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(PhaseEntry);
    }
}

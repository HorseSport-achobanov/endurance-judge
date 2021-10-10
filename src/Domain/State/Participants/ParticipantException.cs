using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.State.Participants
{
    public class ParticipantException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(Participant);
    }
}

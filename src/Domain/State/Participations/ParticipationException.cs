using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.State.Participations
{
    public class ParticipationException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(Participation);
    }
}

using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.State.Athletes
{
    public class AthleteException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(Athlete);
    }
}

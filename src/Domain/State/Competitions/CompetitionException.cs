using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.State.Competitions
{
    public class CompetitionException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(Competition);
    }
}

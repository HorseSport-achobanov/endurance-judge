using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.State.Personnels
{
    public class PersonnelException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(Personnel);
    }
}

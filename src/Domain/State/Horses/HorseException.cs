using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.State.Horses
{
    public class HorseException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(Horse);
    }
}

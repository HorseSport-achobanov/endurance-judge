using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Categorizations
{
    public class CategorizationObjectException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(Categorization);
    }
}

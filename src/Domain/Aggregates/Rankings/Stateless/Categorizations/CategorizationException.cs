using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Categorizations
{
    public class CategorizationException : DomainException
    {
        protected override string Entity { get; } = nameof(Categorization);
    }
}

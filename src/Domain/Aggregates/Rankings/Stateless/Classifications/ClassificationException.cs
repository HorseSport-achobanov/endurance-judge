using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Classifications
{
    public class ClassificationException : DomainException
    {
        protected override string Entity { get; } = nameof(Classification);
    }
}

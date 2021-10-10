using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Classifications
{
    public class ClassificationObjectException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(Classification);
    }
}

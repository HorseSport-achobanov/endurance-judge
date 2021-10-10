using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Import
{
    public class ImportObjectException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(ImportManager);
    }
}

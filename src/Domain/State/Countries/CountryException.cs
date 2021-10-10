using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.State.Countries
{
    public class CountryException : DomainObjectException
    {
        protected override string Entity { get; } = nameof(Country);
    }
}

namespace EnduranceJudge.Domain.Core.Exceptions;

public class NewDomainException : DomainExceptionBase
{
    protected override string Entity => "New";
}

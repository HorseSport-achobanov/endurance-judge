using EnduranceJudge.Domain.Core.Models;

namespace EnduranceJudge.Domain.AggregateRoots.PoC.Events;

public delegate void DomainChangedHandler<T>(object sender, DomainChangedArguments arguments);

public class DomainChangedArguments
{
    public DomainChangedArguments(params IDomain[] obj)
    {
        this.Objects = obj;
    }
    
    public IDomain[] Objects { get; }
}

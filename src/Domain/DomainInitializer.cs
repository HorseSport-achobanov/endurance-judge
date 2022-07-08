using EnduranceJudge.Core.Services;
using EnduranceJudge.Domain.AggregateRoots.PoC.Root;

namespace EnduranceJudge.Domain;

public class DomainInitializer : IInitializer
{
    public DomainInitializer(PocRoot pocRoot) // Create the singleton instance.
    {
        ;
    }
    
    public int RunningOrder => 20;
    public void Run()
    {
    }
}

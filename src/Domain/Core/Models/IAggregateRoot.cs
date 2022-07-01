using EnduranceJudge.Core.ConventionalServices;

namespace EnduranceJudge.Domain.Core.Models;

// TODO: Make Singleton
public interface IAggregateRoot : IAggregate, ITransientService
{
}

public interface INewAggregateRoot : IAggregate, ISingletonService
{
}

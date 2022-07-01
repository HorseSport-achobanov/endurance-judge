namespace EnduranceJudge.Domain.AggregateRoots.PoC.Events;

public delegate void DomainChangedHandler<T>(object sender, StateChangeArguments<T> arguments);

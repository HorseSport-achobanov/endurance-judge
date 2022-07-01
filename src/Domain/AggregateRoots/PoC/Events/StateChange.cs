using EnduranceJudge.Domain.Annotations;

namespace EnduranceJudge.Domain.AggregateRoots.PoC.Events;

public delegate void StateChangeHandler<T>([CanBeNull] object sender, StateChangeArguments<T> args);

public class StateChangeArguments<T>
{
    public StateChangeArguments(T changedObject)
    {
        this.ChangedObject = changedObject;
        // this.Old = old;
    }
    
    public T ChangedObject { get; }
    // public T Old { get; }
}

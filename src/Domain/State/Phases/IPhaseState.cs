using EnduranceJudge.Domain.Core.Models;

namespace EnduranceJudge.Domain.State.Phases
{
    public interface IPhaseState : IDomainModelState
    {
        int LengthInKm { get; }
        bool IsFinal { get; }
        int OrderBy { get; }
        int MaxRecoveryTimeInMins { get; }
        int RestTimeInMins { get; }
    }
}

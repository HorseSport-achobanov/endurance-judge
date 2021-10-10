using EnduranceJudge.Domain.Core.Models;
using System;

namespace EnduranceJudge.Domain.State.PhaseEntries
{
    public interface IPhaseEntryState : IDomainModelState
    {
        DateTime StartTime { get; }
        DateTime? ArrivalTime { get; }
        DateTime? InspectionTime { get; }
        DateTime? ReInspectionTime { get; }
    }
}

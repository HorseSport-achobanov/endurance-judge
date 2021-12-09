using EnduranceJudge.Domain.Core.Models;
using System;

namespace EnduranceJudge.Domain.State.Performances
{
    public interface IPerformanceState : IDomainModelState
    {
        DateTime StartTime { get; }
        DateTime? ArrivalTime { get; }
        DateTime? InspectionTime { get; }
        DateTime? ReInspectionTime { get; }
        bool IsAnotherInspectionRequired { get; }
        DateTime? RequiredInspectionTime { get; }
        DateTime? NextPerformanceStartTime { get; }
    }
}

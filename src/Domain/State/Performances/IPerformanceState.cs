using EnduranceJudge.Core.Models;
using System;

namespace EnduranceJudge.Domain.State.Performances
{
    public interface IPerformanceState : IIdentifiable
    {
        DateTime StartTime { get; }
        DateTime? ArrivalTime { get; }
        DateTime? InspectionTime { get; }
        DateTime? ReInspectionTime { get; }
        DateTime? RequiredInspectionTime { get; }
        DateTime? NextPerformanceStartTime { get; }
        bool IsAnotherInspectionRequired { get; }
    }
}
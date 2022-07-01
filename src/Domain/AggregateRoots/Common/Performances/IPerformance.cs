using EnduranceJudge.Domain.State.LapRecords;
using System;

namespace EnduranceJudge.Domain.AggregateRoots.Common.Performances;

public interface IPerformance : ILapRecordState
{
    DateTime? RequiredInspectionTime { get; }
    TimeSpan? RecoverySpan { get; }
    TimeSpan? Time { get; }
    double? AverageSpeed { get; }
    double? AverageSpeedTotal { get; }
    public double TotalLength { get; }
    public DateTime? NextStartTime { get; }
}

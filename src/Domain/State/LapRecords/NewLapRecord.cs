using EnduranceJudge.Domain.AggregateRoots.Common.Performances;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State.Laps;
using EnduranceJudge.Domain.State.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using static EnduranceJudge.Localization.Strings;

namespace EnduranceJudge.Domain.State.LapRecords;

public class NewLapRecord : NewDomainBase, IPerformance
{
    private DateTime startTime;
    private DateTime? arrivalTime;
    private DateTime? inspectionTime;
    private DateTime? reInspectionTime;
    private bool isReInspectionRequired;
    private bool isRequiredInspectionRequired;
    private DateTime? requiredInspectionTime;
    private DateTime? nextStartTime;
    private TimeSpan? recoverySpan;
    private TimeSpan? time;
    private double? averageSpeed;
    private double? averageSpeedTotal;
    private double totalLength;
    private Result result;

    private NewLapRecord()
    {
    }
    public NewLapRecord(
        DateTime startTime,
        DateTime? arrivalTime,
        ILapState lap,
        IEnumerable<double> previousLengths,
        IEnumerable<double> previousAverageSpeeds) : base(GENERATE_ID)
    {
        this.StartTime = startTime;
        this.ArrivalTime = arrivalTime;
        this.IsFinal = lap.IsFinal;
        this.LengthInKm = lap.LengthInKm;
        this.RestTimeInMins = lap.RestTimeInMins;
        this.IsCompulsoryInspectionRequired = lap.IsCompulsoryInspectionRequired;
        this.PreviousLengths = previousLengths.ToList();
        this.PreviousAverageSpeeds = previousAverageSpeeds.ToList();
    }
    
    public bool IsFinal { get; private set; }
    public double LengthInKm { get; private set; }
    public int RestTimeInMins { get; private set; }
    public bool IsCompulsoryInspectionRequired { get; private set; }

    public DateTime StartTime
    {
        get => this.startTime;
        set => this.SetValue(ref this.startTime, value);
    }
    public DateTime? ArrivalTime
    {
        get => this.arrivalTime;
        set => this.SetValue(ref this.arrivalTime, value);
    }
    public DateTime? InspectionTime
    {
        get => this.inspectionTime;
        set => this.SetValue(ref this.inspectionTime, value);
    }
    public DateTime? ReInspectionTime
    {
        get => this.reInspectionTime;
        set => this.SetValue(ref this.reInspectionTime, value);
    }
    public bool IsReInspectionRequired
    {
        get => this.isReInspectionRequired;
        set => this.SetValue(ref this.isReInspectionRequired, value);
    }
    public bool IsRequiredInspectionRequired
    {
        get => this.isRequiredInspectionRequired;
        set => this.SetValue(ref this.isRequiredInspectionRequired, value);
    }
    public DateTime? RequiredInspectionTime
    {
        get => this.requiredInspectionTime;
        internal set => this.SetValue(ref this.requiredInspectionTime, value);
    }
    public DateTime? NextStartTime
    {
        get => this.nextStartTime;
        internal set => this.SetValue(ref this.nextStartTime, value);
    }
    public TimeSpan? RecoverySpan
    {
        get => this.recoverySpan;
        internal set => this.SetValue(ref this.recoverySpan, value);
    }
    public TimeSpan? Time
    {
        get => this.time;
        internal set => this.SetValue(ref this.time, value);
    }
    public double? AverageSpeed
    {
        get => this.averageSpeed;
        internal set => this.SetValue(ref this.averageSpeed, value);
    }
    public double? AverageSpeedTotal
    {
        get => this.averageSpeedTotal;
        internal set => this.SetValue(ref this.averageSpeedTotal, value);
    }
    public double TotalLength
    {
        get => this.totalLength;
        internal set => this.SetValue(ref this.totalLength, value);
    }
    public Result Result
    {
        get => this.result;
        internal set => this.SetValue(ref this.result, value);
    }
    public string Header
    {
        get
        {
            var lap = this.IsFinal
                ? $"{FINAL}"
                : $"{GATE.ToUpper()}{this.PreviousLengths.Count + 1}";
            var header = $"{lap}/{this.TotalLength} {KM}";
            return header;
        }
    }

    public List<double> PreviousLengths { get; private set; } = new();
    public List<double> PreviousAverageSpeeds { get; private set; } = new();
}

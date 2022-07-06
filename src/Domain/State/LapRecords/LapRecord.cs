using EnduranceJudge.Domain.AggregateRoots.Manager.Aggregates;
using EnduranceJudge.Domain.Annotations;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State.Laps;
using EnduranceJudge.Domain.State.Results;
using System;
using System.ComponentModel;

namespace EnduranceJudge.Domain.State.LapRecords;

public class LapRecord : DomainBase<LapRecordException>, ILapRecordState, INotifyPropertyChanged
{
    public LapRecord() {}
    public LapRecord(DateTime startTime, Lap lap) : base(GENERATE_ID)
    {
        this.StartTime = startTime;
        this.Lap = lap;
    }

    private DateTime? _arrivalTime;
    private DateTime? _inspectionTime;
    private DateTime? _reInspectionTime;
    
    public Lap Lap { get; private set; }
    public DateTime StartTime { get; set; } // TODO: set to private/internal after testing
    public DateTime? ArrivalTime
    {
        get => this._arrivalTime;
        set => this.SetProperty(ref this._arrivalTime, value, nameof(this.ArrivalTime)); // TODO: set to private/internal after testing
    }
    public DateTime? InspectionTime
    {
        get => this._inspectionTime;
        set => this.SetProperty(ref this._inspectionTime, value, nameof(this.InspectionTime)); // TODO: set to private/internal after testing
    }
    public DateTime? ReInspectionTime
    {
        get => this._reInspectionTime; 
        set => this.SetProperty(ref this._reInspectionTime, value, nameof(this.ReInspectionTime)); // TODO: set to private/internal after testing
    }
    public bool IsReInspectionRequired { get; internal set; }
    public bool IsRequiredInspectionRequired { get; internal set; }
    public Result Result { get; internal set; }

    public DateTime? VetGateTime
        => this.ReInspectionTime ?? this.InspectionTime;
    public DateTime? NextStarTime
        => this.VetGateTime?.AddMinutes(this.Lap.RestTimeInMins);
    public event PropertyChangedEventHandler PropertyChanged;
    
    [NotifyPropertyChangedInvocator]
    protected virtual void RaisePropertyChanged(string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetProperty<T>(ref T property, T value, string name)
    {
        if (!value.Equals(property))
        {
            property = value;
            this.RaisePropertyChanged(name);
        }
    }
}

public static partial class AggregateExtensions
{
    public static LapRecordsAggregate Aggregate(this LapRecord record)
        => new (record);
}

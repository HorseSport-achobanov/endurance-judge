using EnduranceJudge.Domain.Core.Models;
using System;

namespace EnduranceJudge.Domain.State.LapRecords;

public class NewLapRecord : NewDomainBase, ILapRecordState
{
    private DateTime startTime;
    private DateTime? arrivalTime;
    private DateTime? inspectionTime;
    private DateTime? reInspectionTime;
    private bool isReInspectionRequired;
    private bool isRequiredInspectionRequired;
    
    public NewLapRecord(
        DateTime startTime,
        DateTime? arrivalTime,
        DateTime? inspectionTime = null,
        DateTime? reInspectionTime = null,
        bool? isReInspectionRequired = null,
        bool? isRequiredInspectionRequired = null)
    {
        this.StartTime = startTime;
        this.ArrivalTime = arrivalTime;
        this.InspectionTime = inspectionTime;
        this.ReInspectionTime = reInspectionTime;
        this.IsReInspectionRequired = isReInspectionRequired ?? this.IsReInspectionRequired;
        this.IsRequiredInspectionRequired = isRequiredInspectionRequired ?? this.IsRequiredInspectionRequired;
    }

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
}

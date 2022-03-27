﻿using EnduranceJudge.Core.Utilities;
using EnduranceJudge.Domain.AggregateRoots.Manager;
using EnduranceJudge.Domain.AggregateRoots.Common.Performances;
using EnduranceJudge.Domain.State.LapRecords;
using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Services;
using Prism.Commands;
using System;
using System.Windows;
using static EnduranceJudge.Localization.Strings;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Common.Performances;

public class PerformanceTemplateModel : ViewModelBase, ILapRecordState
{
    private readonly IExecutor<ManagerRoot> managerExecutor;
    public PerformanceTemplateModel(Performance performance)
    {
        this.EditVisibility = Visibility.Visible;
        this.managerExecutor = StaticProvider.GetService<IExecutor<ManagerRoot>>();
        this.Edit = new DelegateCommand(this.EditAction);
        this.HeaderValue = $"{GATE.ToUpper()}{performance.Index}/{this.TotalLength} {KM}";
        this.Update(performance);
    }

    public Visibility EditVisibility { get; set; }
    public Visibility ReadonlyVisibility => this.EditVisibility == Visibility.Collapsed
        ? Visibility.Visible
        : Visibility.Collapsed;

    public DelegateCommand Edit { get; }

    public string HeaderValue { get; }
    private DateTime startTime;
    private string arrivalTimeString;
    private string inspectionTimeString;
    private string reInspectionTimeString;
    private string requiredInspectionTimeString;
    private string compulsoryRequiredInspectionTimeString;
    private string recoverySpanString;
    private string timeString;
    public string nextStartTimeString;
    private string averageSpeedForLoopKpHString;
    private string averageSpeedTotalKpHString;

    public void EditAction()
    {
        var result = this.managerExecutor.Execute(x => x.EditRecord(this));
        this.Update(result);
    }

    public void Update(Performance performance)
    {
        this.Id = performance.Id;
        this.RequiredInspectionTimeString = ValueSerializer.FormatTime(performance.RequiredInspectionTime);
        this.RecoverySpanString = ValueSerializer.FormatSpan(performance.RecoverySpan);
        this.TimeString = ValueSerializer.FormatSpan(performance.Time);
        this.AverageSpeed = performance.AverageSpeed;
        this.AverageSpeedTotalString = ValueSerializer.FormatDouble(performance.AverageSpeedTotal);
        this.TotalLength = performance.TotalLength;
        this.NextStartTimeString = ValueSerializer.FormatTime(performance.NextStartTime);
        this.StartTime = performance.StartTime;
        this.ArrivalTime = performance.ArrivalTime;
        this.InspectionTime = performance.InspectionTime;
        this.IsReInspectionRequired = performance.IsReInspectionRequired;
        this.IsRequiredInspectionRequired = performance.IsRequiredInspectionRequired;
        var requiredInspectionTime = ValueSerializer.FormatTime(performance.RequiredInspectionTime);
        this.RequiredInspectionTimeString = performance.Lap.IsCompulsoryInspectionRequired
            ? string.Empty
            : requiredInspectionTime;
        this.CompulsoryRequiredInspectionTimeString = performance.Lap.IsCompulsoryInspectionRequired
            ? requiredInspectionTime
            : string.Empty;
    }

#region IPerformanceState implementation

    public DateTime? ArrivalTime
    {
        get => ValueSerializer.ParseTime(this.ArrivalTimeString);
        private set => this.ArrivalTimeString = ValueSerializer.FormatTime(value);
    }
    public DateTime? InspectionTime
    {
        get => ValueSerializer.ParseTime(this.InspectionTimeString);
        private set => this.InspectionTimeString = ValueSerializer.FormatTime(value);
    }
    public DateTime? ReInspectionTime
    {
        get => ValueSerializer.ParseTime(this.ReInspectionTimeString);
        private set => this.ReInspectionTimeString = ValueSerializer.FormatTime(value);
    }
    public double? AverageSpeed
    {
        get => ValueSerializer.ParseDouble(this.AverageSpeedString);
        private set => this.AverageSpeedString = ValueSerializer.FormatDouble(value);
    }
    public bool IsReInspectionRequired { get; private set; }
    public bool IsRequiredInspectionRequired { get; private set; }
    public double TotalLength { get; private set; }
    public int Id { get; private set; }

#endregion

#region Setters

    public DateTime StartTime
    {
        get => this.startTime;
        private set => this.SetProperty(ref this.startTime, value);
    }
    public string ArrivalTimeString
    {
        get => this.arrivalTimeString;
        set => this.SetProperty(ref this.arrivalTimeString, value);
    }
    public string InspectionTimeString
    {
        get => this.inspectionTimeString;
        set => this.SetProperty(ref this.inspectionTimeString, value);
    }
    public string ReInspectionTimeString
    {
        get => this.reInspectionTimeString;
        set => this.SetProperty(ref this.reInspectionTimeString, value);
    }
    public string RequiredInspectionTimeString
    {
        get => this.requiredInspectionTimeString;
        set => this.SetProperty(ref this.requiredInspectionTimeString, value);
    }
    public string CompulsoryRequiredInspectionTimeString
    {
        get => this.compulsoryRequiredInspectionTimeString;
        set => this.SetProperty(ref this.compulsoryRequiredInspectionTimeString, value);
    }
    public string RecoverySpanString
    {
        get => this.recoverySpanString;
        private set => this.SetProperty(ref this.recoverySpanString, value);
    }
    public string TimeString
    {
        get => this.timeString;
        private set => this.SetProperty(ref this.timeString, value);
    }
    public string AverageSpeedString
    {
        get => this.averageSpeedForLoopKpHString;
        private set => this.SetProperty(ref this.averageSpeedForLoopKpHString, value);
    }
    public string AverageSpeedTotalString
    {
        get => this.averageSpeedTotalKpHString;
        private set => this.SetProperty(ref this.averageSpeedTotalKpHString, value);
    }
    public string NextStartTimeString
    {
        get => this.nextStartTimeString;
        private set => this.SetProperty(ref this.nextStartTimeString, value);
    }

#endregion Setters;
}

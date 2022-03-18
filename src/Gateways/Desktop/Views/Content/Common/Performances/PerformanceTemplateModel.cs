﻿using EnduranceJudge.Application.Core.Services;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Utilities;
using EnduranceJudge.Domain.AggregateRoots.Manager;
using EnduranceJudge.Domain.AggregateRoots.Common.Performances;
using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Services;
using Prism.Commands;
using System;
using System.Windows;
using static EnduranceJudge.Gateways.Desktop.DesktopConstants;
using static EnduranceJudge.Localization.Strings;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Common.Performances;

public class PerformanceTemplateModel : ViewModelBase, IMapFrom<Performance>, IPerformance
{
    private readonly IExecutor<ManagerRoot> managerExecutor;
    private readonly IDateService dateService;
    private bool showEdit;

    public PerformanceTemplateModel(Performance performance, bool showEdit)
    {
        this.showEdit = showEdit;
        this.managerExecutor = StaticProvider.GetService<IExecutor<ManagerRoot>>();
        this.dateService = StaticProvider.GetService<IDateService>();
        this.Edit = new DelegateCommand(this.EditAction);
        this.HeaderValue = $"{GATE.ToUpper()}{performance.Index}/{this.TotalLength} {KM}";
        this.MapFrom(performance); // TODO probably remove
    }

    public Visibility EditVisibility => this.showEdit
        ? Visibility.Visible
        : Visibility.Collapsed;
    public Visibility ReadonlyVisibility => this.showEdit
        ? Visibility.Collapsed
        : Visibility.Visible;

    public DelegateCommand Edit { get; }

    public string HeaderValue { get; }
    private DateTime startTime;
    private string arrivalTimeString;
    private string inspectionTimeString;
    private string reInspectionTimeString;
    private string requiredInspectionTimeString;
    private string compulsoryRequiredInspectionTimeString;
    private TimeSpan? recoverySpan;
    private TimeSpan? time;
    private double? averageSpeedForLoopKpH;
    private double? averageSpeedTotalKpH;
    public DateTime? nextStartTime;

    public void ToggleEditVisibility()
    {
        this.showEdit = !this.showEdit;
    }

    public void EditAction()
    {
        var result = this.managerExecutor.Execute(x => x.EditRecord(this));
        this.MapFrom(result);
    }

    #region IPerformanceState implementation

    public DateTime? ArrivalTime
    {
        get => this.ParseTime(this.ArrivalTimeString);
        private set => this.ArrivalTimeString = this.FormatTime(value);
    }
    public DateTime? InspectionTime
    {
        get => this.ParseTime(this.InspectionTimeString);
        private set => this.InspectionTimeString = this.FormatTime(value);
    }
    public DateTime? ReInspectionTime
    {
        get => this.ParseTime(this.ReInspectionTimeString);
        private set => this.ReInspectionTimeString = this.FormatTime(value);
    }
    public int Index { get; }
    public DateTime? RequiredInspectionTime
    {
        get => this.ParseTime(this.RequiredInspectionTimeString);
        private set => this.RequiredInspectionTimeString = this.FormatTime(value);
    }
    public DateTime? CompulsoryRequiredInspectionTime // Possibly remove
    {
        get => this.ParseTime(this.CompulsoryRequiredInspectionTimeString);
        private set => this.CompulsoryRequiredInspectionTimeString = this.FormatTime(value);
    }
    public bool IsReInspectionRequired { get; private set; }
    public bool IsRequiredInspectionRequired { get; private set; }
    public double TotalLength { get; private set;  }
    public int Id { get; private set; }

    #endregion

    private DateTime? ParseTime(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }
        var date = this.dateService.Parse(value, TIME_FORMAT);
        return date;
    }
    private string FormatTime(DateTime? time)
    {
        var timeString = time?.ToString(TIME_FORMAT);
        return timeString;
    }

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
    public TimeSpan? RecoverySpan
    {
        get => this.recoverySpan;
        private set => this.SetProperty(ref this.recoverySpan, value);
    }
    public TimeSpan? Time
    {
        get => this.time;
        private set => this.SetProperty(ref this.time, value);
    }
    public double? AverageSpeed
    {
        get => this.averageSpeedForLoopKpH;
        private set => this.SetProperty(ref this.averageSpeedForLoopKpH, value);
    }
    public double? AverageSpeedTotal
    {
        get => this.averageSpeedTotalKpH;
        private set => this.SetProperty(ref this.averageSpeedTotalKpH, value);
    }
    public DateTime? NextStartTime
    {
        get => this.nextStartTime;
        private set => this.SetProperty(ref this.nextStartTime, value);
    }

    #endregion Setters;
}

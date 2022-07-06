using EnduranceJudge.Application.Core;
using EnduranceJudge.Domain.AggregateRoots.PoC.Events;
using EnduranceJudge.Domain.AggregateRoots.PoC.Root;
using EnduranceJudge.Domain.State.LapRecords;
using EnduranceJudge.Domain.State.Participations;
using EnduranceJudge.Domain.State.Results;
using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Core.Services;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.PoC;

public class PocViewModel : ViewModelBase
{
    private readonly IQueries<Participation> participationQueries;
    private static readonly DateTime Today = DateTime.Today;
    private Participation selectedParticipation;
    private NewLapRecord selectedLap;
    private Visibility startVisibility;
    private string inputNumber;
    private int? inputHours;
    private int? inputMinutes;
    private int? inputSeconds;
    private string notQualifiedReason;
    
    public PocViewModel(IPopupService popupService, IQueries<Participation> participationQueries)
    {
        this.participationQueries = participationQueries;
        this.Update = new DelegateCommand(this.UpdateAction);
        this.Start = new DelegateCommand(this.StartAction);
        this.Disqualify = new DelegateCommand(() => this.DisqualifyAction(ResultType.Disqualified));
        this.FailToQualify = new DelegateCommand(() => this.DisqualifyAction(ResultType.FailedToQualify));
        this.Resign = new DelegateCommand(() => this.DisqualifyAction(ResultType.Resigned));
        this.ReInspection = new DelegateCommand(this.AllowReInspectionAction);
        this.RequireInspection = new DelegateCommand(this.RequireInspectionAction);
        this.StartList = new DelegateCommand(popupService.RenderStartList);
        this.Select = new DelegateCommand<object[]>(list =>
        {
            var obj = list.FirstOrDefault();
            if (obj != null)
            {
                this.SelectAction(obj as Participation);
            }
        });
    }

    public override void OnNavigatedTo(NavigationContext context)
    {
        if (this.Participations.Any())
        {
            return;
        }
        this.Participations = new ObservableCollection<Participation>(this.participationQueries.GetAll());
    }

    public DelegateCommand<object[]> Select { get; }
    public DelegateCommand Start { get; }
    public DelegateCommand Update { get; }
    public DelegateCommand Disqualify { get; }
    public DelegateCommand FailToQualify { get; }
    public DelegateCommand Resign { get; }
    public DelegateCommand ReInspection { get; }
    public DelegateCommand RequireInspection { get; }
    public DelegateCommand StartList { get; }

    public ObservableCollection<Participation> Participations { get; private set; } = new();
    public Participation SelectedParticipation
    {
        get => this.selectedParticipation;
        set => this.SetProperty(ref this.selectedParticipation, value);
    }
    public NewLapRecord SelectedLap
    {
        get => this.selectedLap;
        set => this.SetProperty(ref this.selectedLap, value);
    }

    private void StartAction()
    {
        PoCEvents.RaiseStart();
        this.StartVisibility = Visibility.Collapsed;
    }
    private void UpdateAction()
    {
        PoCEvents.RaiseSetLapTime(this.InputNumber, this.InputTime);
    }
    private void DisqualifyAction(ResultType type)
    {
        PoCEvents.RaiseDisqualify(this.InputNumber, type, this.NotQualifiedReason);
    }

    private void SelectAction(Participation participation)
    {
        this.SelectedParticipation = participation;
        this.SelectedLap = participation.NewParticipant.LapRecords.Last();
    }
    
    private void AllowReInspectionAction()
    {
        this.selectedLap.IsReInspectionRequired = !this.selectedLap.IsReInspectionRequired;
        var args = new StateChangeArguments<NewLapRecord>(this.selectedLap);
        PoCEvents.RaiseLapRecordChange(args);
    }
    private void RequireInspectionAction()
    {
        this.selectedLap.IsRequiredInspectionRequired = !this.selectedLap.IsRequiredInspectionRequired;
        var args = new StateChangeArguments<NewLapRecord>(this.selectedLap);
        PoCEvents.RaiseLapRecordChange(args);
    }

    public Visibility StartVisibility
    {
        get => this.startVisibility;
        set => this.SetProperty(ref this.startVisibility, value);
    }
    public string InputNumber
    {
        get => this.inputNumber;
        set => this.SetProperty(ref this.inputNumber, value);
    }
    public string NotQualifiedReason
    {
        get => this.notQualifiedReason;
        set => this.SetProperty(ref this.notQualifiedReason, value);
    }
    public int? InputHours
    {
        get => this.inputHours;
        set => this.SetProperty(ref this.inputHours, value);
    }
    public int? InputMinutes
    {
        get => this.inputMinutes;
        set => this.SetProperty(ref this.inputMinutes, value);
    }
    public int? InputSeconds
    {
        get => this.inputSeconds;
        set => this.SetProperty(ref this.inputSeconds, value);
    }

    private DateTime InputTime
    {
        get
        {
            var time = Today;
            if (this.InputHours.HasValue)
            {
                time = time.AddHours(this.InputHours.Value);
            }
            if (this.InputMinutes.HasValue)
            {
                time = time.AddMinutes(this.InputMinutes.Value);
            }
            if (this.InputSeconds.HasValue)
            {
                time = time.AddSeconds(this.InputSeconds.Value);
            }
            return time;
        }
    }
}

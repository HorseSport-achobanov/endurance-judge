﻿using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Utilities;
using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Domain.State.Participations;
using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Services;
using EnduranceJudge.Gateways.Desktop.Views.Content.Manager.PhasePerformances;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using static EnduranceJudge.Localization.DesktopStrings;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Manager.Participants
{
    public class ParticipantTemplateModel : ViewModelBase, ICollapsable
    {
        private readonly IPrinter printer;

        public ParticipantTemplateModel(Participant participant, Visibility visibility = Visibility.Visible)
        {
            this.printer = StaticProvider.GetService<IPrinter>();
            this.Visibility = visibility;
            this.Number = participant.Number;
            this.UpdatePhases(participant.Participation);
            this.ToggleVisibility = new DelegateCommand(this.ToggleVisibilityAction);
            this.Print = new DelegateCommand<Visual>(this.PrintAction);
        }

        public DelegateCommand<Visual> Print { get; }
        public DelegateCommand ToggleVisibility { get; }
        private string toggleText = EXPAND;
        private readonly int number;
        private Visibility visibility;
        public ObservableCollection<PerformanceTemplateModel> Performances { get; } = new();
        public ObservableCollection<double> PhaseLengths { get; } = new();

        private void UpdatePhases(Participation participation)
        {
            var lengths = participation.Performances.Select(x => x.Phase.LengthInKm);
            var viewModels = participation.Performances.MapEnumerable<PerformanceTemplateModel>();

            this.Performances.AddRange(viewModels);
            this.PhaseLengths.AddRange(lengths);
        }

        private void PrintAction(Visual control)
        {
            this.printer.Print(control);
        }

        public int Number
        {
            get => this.number;
            private init => this.SetProperty(ref this.number, value);
        }
        public Visibility Visibility
        {
            get => this.visibility;
            set => this.SetProperty(ref this.visibility, value);
        }
        public string ToggleText
        {
            get => this.toggleText;
            set => this.SetProperty(ref this.toggleText, value);
        }

        private void ToggleVisibilityAction()
        {
            if (this.Visibility == Visibility.Collapsed)
            {
                this.Visibility = Visibility.Visible;
                this.ToggleText = COLLAPSE;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
                this.ToggleText = EXPAND;
            }
        }
    }
}
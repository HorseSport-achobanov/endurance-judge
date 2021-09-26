using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.States;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.SimpleListItem;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Participants;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Phases;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Competitions
{
    public class CompetitionViewModel : ChildFormBase<CompetitionView>, ICompetitionState
    {
        public CompetitionViewModel()
        {
            this.LoadTypes();
            this.NavigateToCreatePhase = new DelegateCommand(this.NavigateToNewChild<PhaseView>);
            this.NavigateToCreateParticipant = new DelegateCommand(this.NavigateToNewChild<ParticipantView>);
        }

        public DelegateCommand NavigateToCreatePhase { get; }
        public DelegateCommand NavigateToCreateParticipant { get; }
        public ObservableCollection<SimpleListItemViewModel> TypeItems { get; private set; }
        public ObservableCollection<PhaseViewModel> Phases { get; } = new();
        public ObservableCollection<ParticipantViewModel> Participants { get; } = new();

        private int typeValue;
        private string typeString;
        private string name;
        private DateTime startTime = DateTime.Today;
        public CompetitionType Type => (CompetitionType)this.TypeValue;

        public string TypeString
        {
            get => this.typeString;
            set => this.SetProperty(ref this.typeString, value);
        }
        public int TypeValue
        {
            get => this.typeValue;
            set
            {
                this.SetProperty(ref this.typeValue, value);
                this.TypeString = ((CompetitionType)this.typeValue).ToString();
            }
        }
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }
        public DateTime StartTime
        {
            get => this.startTime;
            set => this.SetProperty(ref this.startTime, value);
        }

        public override void HandleChildren(NavigationContext context)
        {
            this.AddOrUpdateChild(context, this.Phases);
            this.AddOrUpdateChild(context, this.Participants);

            this.UpdateGrandChild(context, this.Phases);
        }

        private void LoadTypes()
        {
            var typeViewModels = SimpleListItemViewModel.FromEnum<CompetitionType>();
            this.TypeItems = new ObservableCollection<SimpleListItemViewModel>(typeViewModels);
        }
    }
}

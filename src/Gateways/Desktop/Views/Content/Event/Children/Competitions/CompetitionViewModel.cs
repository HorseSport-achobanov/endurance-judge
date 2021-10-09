﻿using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.SimpleListItem;
using EnduranceJudge.Gateways.Desktop.Core.Static;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Events.Athletes;
using EnduranceJudge.Gateways.Desktop.Events.Horses;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Participants;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Phases;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Competitions
{
    public class CompetitionViewModel : FormBase<CompetitionView>, ICompetitionState
    {
        private readonly IEventAggregator eventAggregator;

        public CompetitionViewModel()
        {
            this.LoadTypes();
            this.NavigateToCreatePhase = new DelegateCommand(this.Navigation.ChangeTo<PhaseView>);
            this.NavigateToCreateParticipant = new DelegateCommand(this.Navigation.ChangeTo<ParticipantView>);
            this.Toggle = new DelegateCommand(this.ToggleAction);
            this.eventAggregator = ServiceProvider.GetService<IEventAggregator>();
            this.ListenForCompetitorChange();
        }

        public DelegateCommand Toggle { get; }
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
        private string toggleText = "Expand";
        private Visibility toggleVisibility = Visibility.Collapsed;

        protected override void Load(int id)
        {
            throw new NotImplementedException();
        }
        protected override void SubmitAction()
        {
            throw new NotImplementedException();
        }

        private void ToggleAction()
        {
            if (this.ToggleVisibility == Visibility.Collapsed)
            {
                this.ToggleVisibility = Visibility.Visible;
                this.ToggleText = "Collapse";
            }
            else
            {
                this.ToggleVisibility = Visibility.Collapsed;
                this.ToggleText = "Expand";
            }
        }

        private void ListenForCompetitorChange()
        {
            this.eventAggregator
                .GetEvent<AthleteRemovedEvent>()
                .Subscribe(this.HandleRemovedAthlete);
            this.eventAggregator
                .GetEvent<AthleteUpdatedEvent>()
                .Subscribe(this.HandleUpdatedAthlete);
            this.eventAggregator
                .GetEvent<HorseRemovedEvent>()
                .Subscribe(this.HandleRemovedHorse);
            this.eventAggregator
                .GetEvent<HorseUpdatedEvent>()
                .Subscribe(this.HandleUpdatedHorse);
        }

        private void HandleRemovedHorse(int horseId)
        {
            var participant = this.Participants.FirstOrDefault(p => p.HorseId == horseId);
            if (participant != null)
            {
                this.Participants.Remove(participant);
            }
        }
        private void HandleRemovedAthlete(int athleteId)
        {
            var participant = this.Participants.FirstOrDefault(p => p.AthleteId == athleteId);
            if (participant != null)
            {
                this.Participants.Remove(participant);
            }
        }
        private void HandleUpdatedAthlete(IAthleteState athlete)
        {
            var participant = this.Participants.FirstOrDefault(p => p.HorseId == athlete.Id);
            if (participant != null)
            {
                participant.AthleteId = athlete.Id;
                participant.AthleteName = string.Format(
                    DesktopConstants.COMPOSITE_NAME_FORMAT,
                    athlete.FirstName,
                    athlete.LastName);
            }
        }
        private void HandleUpdatedHorse(IHorseState horse)
        {
            var participant = this.Participants.FirstOrDefault(p => p.HorseId == horse.Id);
            if (participant != null)
            {
                participant.HorseId = horse.Id;
                participant.HorseName = horse.Name;
            }
        }

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
        public string ToggleText
        {
            get => this.toggleText;
            set => this.SetProperty(ref this.toggleText, value);
        }
        public Visibility ToggleVisibility
        {
            get => this.toggleVisibility;
            private set => this.SetProperty(ref this.toggleVisibility, value);
        }

        private void LoadTypes()
        {
            var typeViewModels = SimpleListItemViewModel.FromEnum<CompetitionType>();
            this.TypeItems = new ObservableCollection<SimpleListItemViewModel>(typeViewModels);
        }
    }
}

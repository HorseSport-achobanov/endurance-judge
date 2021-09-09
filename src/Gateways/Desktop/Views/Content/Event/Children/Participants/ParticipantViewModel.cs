﻿using EnduranceJudge.Application.Events.Common;
using EnduranceJudge.Application.Events.Queries.GetAthletesList;
using EnduranceJudge.Application.Events.Queries.GetHorseList;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.SimpleListItem;
using EnduranceJudge.Gateways.Desktop.Core.Services;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Services;
using Prism.Commands;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Participants
{
    public class ParticipantViewModel : ChildFormBase<ParticipantView>,
        IMap<ParticipantDependantModel>
    {
        private readonly IApplicationService application;
        private ParticipantViewModel()
        {
        }

        public ParticipantViewModel(IApplicationService application)
        {
            this.application = application;
            this.ToggleIsAverageSpeedInKmPhVisibility = new DelegateCommand(
                this.ToggleIsAverageSpeedInKmPhVisibilityAction);
        }

        public ObservableCollection<SimpleListItemViewModel> HorseItems { get; } = new();
        public ObservableCollection<SimpleListItemViewModel> AthleteItems { get; } = new();

        private string rfId;
        public int number;
        public int? maxAverageSpeedInKmPh;
        private int horseId;
        private int athleteId;
        private int categoryId;

        public string RfId
        {
            get => this.rfId;
            set => this.SetProperty(ref this.rfId, value);
        }
        public int Number
        {
            get => this.number;
            set => this.SetProperty(ref this.number, value);
        }
        public int? MaxAverageSpeedInKmPh
        {
            get => this.maxAverageSpeedInKmPh;
            set => this.SetProperty(ref this.maxAverageSpeedInKmPh, value);
        }
        public int HorseId
        {
            get => this.horseId;
            set => this.SetProperty(ref this.horseId, value);
        }
        public int AthleteId
        {
            get => this.athleteId;
            set => this.SetProperty(ref this.athleteId, value);
        }
        public int CategoryId
        {
            get => this.categoryId;
            set => this.SetProperty(ref this.categoryId, value);
        }

        public override void OnNavigatedTo(NavigationContext context)
        {
            base.OnNavigatedTo(context);
            this.LoadCompetitors();
            if (this.MaxAverageSpeedInKmPh.HasValue)
            {
                this.ShowMaxAverageSpeedInKmPh();
            }
        }

        public DelegateCommand ToggleIsAverageSpeedInKmPhVisibility { get; private set; }
        public void ToggleIsAverageSpeedInKmPhVisibilityAction()
        {
            if (this.MaxAverageSpeedInKmPhVisibility == Visibility.Hidden)
            {
                this.ShowMaxAverageSpeedInKmPh();
            }
            else
            {
                this.HideMaxAverageSpeedInKmPh();
            }
        }
        private void ShowMaxAverageSpeedInKmPh()
        {
            this.MaxAverageSpeedInKmPhVisibility = Visibility.Visible;
        }
        private void HideMaxAverageSpeedInKmPh()
        {
            this.MaxAverageSpeedInKmPhVisibility = Visibility.Hidden;
            this.MaxAverageSpeedInKmPh = default;
        }
        private Visibility maxAverageSpeedInKmPhPhVisibility = Visibility.Hidden;
        public Visibility MaxAverageSpeedInKmPhVisibility
        {
            get => this.maxAverageSpeedInKmPhPhVisibility;
            set => this.SetProperty(ref this.maxAverageSpeedInKmPhPhVisibility, value);
        }

        private string defaultName;
        public string Name
        {
            get
            {
                var athlete = this.AthleteItems.FirstOrDefault(x => x.Id == this.AthleteId);
                var horse = this.HorseItems.FirstOrDefault(x => x.Id == this.HorseId);
                if (athlete != null && horse != null)
                {
                    return $"{athlete.Name} - {horse.Name}";
                }
                return this.defaultName;
            }
            set => this.defaultName = value;
        }

        private async Task LoadCompetitors()
        {
            var horses = await this.application.Execute(new GetHorseList());
            var athletes = await this.application.Execute(new GetAthletesList());

            var horseItems = horses.Select(x => new SimpleListItemViewModel(x));
            var athleteItems = athletes.Select(x => new SimpleListItemViewModel(x));

            this.HorseItems.AddRange(horseItems);
            this.AthleteItems.AddRange(athleteItems);
        }
    }
}
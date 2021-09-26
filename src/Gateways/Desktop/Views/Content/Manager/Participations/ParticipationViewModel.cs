using EnduranceJudge.Application.Actions.Manager.Commands.UpdateParticipation;
using EnduranceJudge.Application.Actions.Manager.Queries.GetParticipation;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Manager.Participations;
using EnduranceJudge.Gateways.Desktop.Core.Static;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Services;
using EnduranceJudge.Gateways.Desktop.Views.Content.Manager.ParticipationsInPhases;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Manager.Participations
{
    public class ParticipationViewModel
        : RootFormBase<GetParticipation, UpdateParticipation, Participation, ParticipationView>
    {
        private readonly IDomainHandler domainHandler;
        private Participation participation;

        public ParticipationViewModel(IApplicationService application, IDomainHandler domainHandler)
            : base(application)
        {
            this.domainHandler = domainHandler;
            this.Start = new DelegateCommand(this.StartAction);
            this.Arrive = new DelegateCommand(this.ArriveAction);
            this.Inspect = new DelegateCommand(this.InspectAction);
            this.ReInspect = new DelegateCommand(this.ReInspectAction);
            this.CompleteSuccessful = new DelegateCommand(this.CompleteSuccessfulAction);
            this.CompleteUnsuccessful = new DelegateCommand(this.CompleteUnsuccessfulAction);
        }

        public DelegateCommand Start { get; }
        public DelegateCommand Arrive { get; }
        public DelegateCommand Inspect { get; }
        public DelegateCommand ReInspect { get; }
        public DelegateCommand CompleteSuccessful { get; }
        public DelegateCommand CompleteUnsuccessful { get; }

        private string number;
        private bool hasExceededSpeedRestriction;
        private bool isComplete;
        public ObservableCollection<ParticipationInPhaseViewModel> ParticipationsInPhases { get; private set; } = new();

        public string Number
        {
            get => this.number;
            set => this.SetProperty(ref this.number, value);
        }
        public bool HasExceededSpeedRestriction
        {
            get => this.hasExceededSpeedRestriction;
            set => this.SetProperty(ref this.hasExceededSpeedRestriction, value);
        }
        public bool IsComplete
        {
            get => this.isComplete;
            set => this.SetProperty(ref this.isComplete, value);
        }

        private void StartAction()
        {
            this.domainHandler.Handle(() => this.participation.Start());
            this.Update();
        }
        private void ArriveAction()
        {
            this.domainHandler.Handle(() => this.participation.Arrive(DateTime.Now));
            this.Update();
        }
        private void InspectAction()
        {
            this.domainHandler.Handle(() => this.participation.Inspect(DateTime.Now));
            this.Update();
        }
        private void ReInspectAction()
        {
            this.domainHandler.Handle(() => this.participation.ReInspect(DateTime.Now));
            this.Update();
        }
        private void CompleteSuccessfulAction()
        {
            this.domainHandler.Handle(() => this.participation.CompleteSuccessful());
            this.Update();
        }
        private void CompleteUnsuccessfulAction()
        {
            this.domainHandler.Handle(() => this.participation.CompleteUnsuccessful("code"));
            this.Update();
        }

        private Visibility startVisibility = Visibility.Collapsed;
        public Visibility StartVisibility
        {
            get => this.startVisibility;
            private set => this.SetProperty(ref this.startVisibility, value);
        }

        private Visibility arriveVisibility = Visibility.Collapsed;
        public Visibility ArriveVisibility
        {
            get => this.arriveVisibility;
            private set => this.SetProperty(ref this.arriveVisibility, value);
        }

        private Visibility inspectVisibility = Visibility.Collapsed;
        public Visibility InspectVisibility
        {
            get => this.inspectVisibility;
            private set => this.SetProperty(ref this.inspectVisibility, value);
        }
        private Visibility reInspectVisibility = Visibility.Collapsed;
        public Visibility ReInspectVisibility
        {
            get => this.reInspectVisibility;
            private set => this.SetProperty(ref this.reInspectVisibility, value);
        }

        private Visibility completeVisibility = Visibility.Collapsed;
        public Visibility CompleteVisibility
        {
            get => this.completeVisibility;
            private set => this.SetProperty(ref this.completeVisibility, value);
        }

        protected override async Task Load(int id)
        {
            if (this.Id != default)
            {
                return;
            }

            var query = new GetParticipation
            {
                Id = id,
            };
            this.participation = await this.Application.Execute(query);
            this.MapFrom(this.participation);
        }

        private async Task Update()
        {
            this.MapFrom(this.participation);
            var update = new UpdateParticipation(this.participation);
            await this.Application.Execute(update);
        }
    }
}

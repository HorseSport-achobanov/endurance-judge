using EnduranceJudge.Domain.Aggregates.Manager.Participations;
using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Services;
using EnduranceJudge.Gateways.Desktop.Views.Content.Manager.Participations;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static EnduranceJudge.Localization.Strings.Desktop;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Manager
{
    public class ManagerViewModel : ViewModelBase
    {
        private static readonly DateTime Today = DateTime.Today;
        private readonly IDomainHandler domainHandler;

        public ManagerViewModel(IDomainHandler domainHandler)
        {
            this.domainHandler = domainHandler;
            this.Update = new DelegateCommand(this.UpdateAction);
            this.Start = new DelegateCommand(this.StartAction);
            this.Complete = new DelegateCommand(this.CompleteAction);
            this.CompleteUnsuccessful = new DelegateCommand(this.CompleteUnsuccessfulAction);
        }

        public DelegateCommand Start { get; }
        public DelegateCommand Update { get; }
        public DelegateCommand Complete { get; }
        public DelegateCommand CompleteUnsuccessful { get; }

        private int inputNumber;
        private int inputHours;
        private int inputMinutes;
        private int inputSeconds;
        private string deQualificationCode;

        public ObservableCollection<ParticipationViewModel> Participations { get; } = new();

        private void StartAction()
        {
            throw new NotImplementedException();
        }
        private void UpdateAction()
        {
            var participation = this.SelectParticipation();
            var time = this.InputTime;
            this.Act(participation, x => x.UpdateProgress(time));
        }
        private void CompleteAction()
        {
            var participation = this.SelectParticipation();
            this.Act(participation, x => x.CompleteSuccessful());
        }
        private void CompleteUnsuccessfulAction()
        {
            var participation = this.SelectParticipation();
            this.Act(participation, x => x.CompleteUnsuccessful(this.DeQualificationCode));
        }

        private Participation SelectParticipation()
        {
            this.CollapseParticipations();
            var participation = this.GetParticipation();
            if (participation == null)
            {
                this.ValidationError(PARTICIPANT_NOT_FOUND);
                return default;
            }
            if (participation.IsComplete)
            {
                this.ValidationError(PARTICIPANT_HAS_COMPLETED_THE_PHASE);
                return default;
            }

            return participation;
        }

        private void CollapseParticipations()
        {
            foreach (var participation in this.Participations)
            {
                participation.Visibility = Visibility.Collapsed;
            }
        }

        private Participation GetParticipation()
        {
            var viewItem = this.Participations.FirstOrDefault(x => x.Number == this.InputNumber);
            Participation participation;
            if (viewItem != null)
            {
                this.Participations.Remove(viewItem);
                participation = viewItem.Participation;
            }
            else
            {
                // var query = new GetParticipation
                // {
                //     Number = this.InputNumber,
                // };
                // participation = this.application.Execute(query).Result;
                participation = default;
            }

            return participation;
        }

        private async Task Act(Participation participation, Action<Participation> update)
        {
            var isValid = this.domainHandler.Handle(() => update(participation));
            if (isValid)
            {
                // var command = new UpdateParticipation(participation);
                // await this.application.Execute(command);
                //
                // var viewModel = new ParticipationViewModel(this.InputNumber, participation);
                // this.Participations.Add(viewModel);
            }
        }

        public int InputNumber
        {
            get => this.inputNumber;
            set => this.SetProperty(ref this.inputNumber, value);
        }
        public string DeQualificationCode
        {
            get => this.deQualificationCode;
            set => this.SetProperty(ref this.deQualificationCode, value);
        }
        public int InputHours
        {
            get => this.inputHours;
            set => this.SetProperty(ref this.inputHours, value);
        }
        public int InputMinutes
        {
            get => this.inputMinutes;
            set => this.SetProperty(ref this.inputMinutes, value);
        }
        public int InputSeconds
        {
            get => this.inputSeconds;
            set => this.SetProperty(ref this.inputSeconds, value);
        }
        private DateTime InputTime => Today
            .AddHours(this.InputHours)
            .AddMinutes(this.InputMinutes)
            .AddSeconds(this.InputSeconds);
    }
}

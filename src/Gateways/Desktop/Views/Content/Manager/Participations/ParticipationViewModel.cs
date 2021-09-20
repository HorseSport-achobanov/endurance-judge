﻿using EnduranceJudge.Application.Actions.Manager.Commands.UpdateParticipation;
using EnduranceJudge.Application.Actions.Manager.Queries.GetParticipation;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Manager.Participations;
using EnduranceJudge.Gateways.Desktop.Core.Static;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using Prism.Commands;
using System;
using System.Threading.Tasks;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Manager.Participations
{
    public class ParticipationViewModel
        : RootFormBase<GetParticipation, UpdateParticipation, Participation, ParticipationView>
    {
        private Participation participation;

        public ParticipationViewModel(IApplicationService application) : base(application)
        {
            this.Start = new DelegateCommand(this.StartAction);
            this.Arrive = new DelegateCommand(this.ArriveAction);
            this.Inspect = new DelegateCommand(this.InspectAction);
            this.ReInspect = new DelegateCommand(this.ReInspectAction);
        }

        public DelegateCommand Start { get; }
        public DelegateCommand Arrive { get; }
        public DelegateCommand Inspect { get; }
        public DelegateCommand ReInspect { get; }

        private string number;
        private bool hasExceededSpeedRestrictions;
        private bool isComplete;

        public string Number
        {
            get => this.number;
            set => this.SetProperty(ref this.number, value);
        }
        public bool HasExceededSpeedRestrictions
        {
            get => this.hasExceededSpeedRestrictions;
            set => this.SetProperty(ref this.hasExceededSpeedRestrictions, value);
        }
        public bool IsComplete
        {
            get => this.isComplete;
            set => this.SetProperty(ref this.isComplete, value);
        }

        private void StartAction()
        {
            this.participation.Start();
            this.Update();
        }
        private void ArriveAction()
        {
            this.participation.Arrive(DateTime.Now);
            this.Update();
        }
        private void InspectAction()
        {
            this.participation.Inspect(DateTime.Now);
            this.Update();
        }
        private void ReInspectAction()
        {
            this.participation.ReInspect(DateTime.Now);
            this.Update();
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
            var update = new UpdateParticipation(this.participation);
            await this.Application.Execute(update);
        }
    }
}
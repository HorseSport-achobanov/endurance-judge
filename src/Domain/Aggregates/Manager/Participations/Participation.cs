using EnduranceJudge.Domain.Aggregates.Manager.ParticipationsInCompetitions;
using EnduranceJudge.Domain.Aggregates.Manager.ParticipationsInPhases;
using EnduranceJudge.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static EnduranceJudge.Localization.Strings.Domain.Manager.Participation;

namespace EnduranceJudge.Domain.Aggregates.Manager.Participations
{
    public class Participation : DomainObjectBase<ManagerParticipationObjectException>
    {
        private List<ParticipationInCompetition> participationsInCompetitions = new();

        private Participation()
        {
        }

        public string Number { get; private set; }
        public IReadOnlyList<ParticipationInCompetition> ParticipationsInCompetitions
        {
            get => this.participationsInCompetitions.AsReadOnly();
            private set => this.participationsInCompetitions = value.ToList();
        }

        public DateTime StartTime => this.ParticipationsInCompetitions
            .First()
            .StartTime;
        public bool HasNotStarted
            => !this.IsComplete
                && this.ParticipationsInCompetitions.All(x => !x.ParticipationsInPhases.Any());
        public bool CanArrive => !this.IsComplete && this.AnyParticipationInPhase(pip => pip.CanArrive);
        public bool CanInspect => !this.IsComplete && this.AnyParticipationInPhase(pip => pip.CanInspect);
        public bool CanReInspect => !this.IsComplete && this.AnyParticipationInPhase(pip => pip.CanReInspect);
        public bool CanComplete => !this.IsComplete && this.AnyParticipationInPhase(pip => pip.CanComplete);
        public bool IsComplete => this.participationsInCompetitions.All(participation => !participation.IsNotComplete);

        public void Start()
            => this.Validate(() =>
            {
                if (!this.HasNotStarted)
                {
                    return;
                }

                foreach (var participation in this.ParticipationsInCompetitions)
                {
                    participation.StartPhase();
                }
            });
        public void UpdateProgress(DateTime time)
        => this.Validate(() =>
        {
            if (this.HasNotStarted)
            {
                this.Throw(HAS_NOT_STARTED);
            }
            if (this.CanComplete)
            {
                this.Throw(CAN_ONLY_BE_COMPLETED);
            }

            if (this.CanArrive)
            {
                this.Arrive(time);
            }
            else if (this.CanInspect)
            {
                this.Inspect(time);
            }
            else if (this.CanReInspect)
            {
                this.ReInspect(time);
            }
        });

        internal void Arrive(DateTime time)
        {
            this.Update(participation => participation.CurrentPhaseEntry.Arrive(time));
        }
        internal void Inspect(DateTime time)
        {
            this.Update(participation => participation.CurrentPhaseEntry.Inspect(time));
        }
        internal void ReInspect(DateTime time)
        {
            this.Update(participation => participation.CurrentPhaseEntry.ReInspect(time));
        }
        public void CompleteSuccessful()
        {
            if (!this.CanComplete)
            {
                this.Throw(CANNOT_BE_COMPLETED);
            }
            this.Update(participation => participation.CompleteSuccessful());
        }
        public void CompleteUnsuccessful(string code)
        {
            if (!this.CanComplete)
            {
                this.Throw(CANNOT_BE_COMPLETED);
            }
            this.Update(participation => participation.CompleteUnsuccessful(code));
        }

        private void Update(Action<ParticipationInCompetition> action)
        {
            foreach (var participation in this.participationsInCompetitions
                .Where(competition => competition.IsNotComplete))
            {
                action(participation);
            }
        }

        private bool AnyParticipationInPhase(Func<PhaseEntryManager, bool> predicate)
        {
            return this.ParticipationsInCompetitions.Any(pic =>
                pic.ParticipationsInPhases.Any(predicate));
        }
    }
}

﻿using EnduranceJudge.Domain.Aggregates.Configuration.Extensions;
using EnduranceJudge.Domain.Core.Exceptions;
using EnduranceJudge.Domain.Core.Extensions;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Domain.State.Participants;
using System.Linq;
using static EnduranceJudge.Localization.Translations.Messages;

namespace EnduranceJudge.Domain.Aggregates.Configuration
{
    public class ParticipantsManager : ManagerObjectBase
    {
        private readonly IState state;

        internal ParticipantsManager(IState state)
        {
            this.state = state;
        }

        public Participant Save(IParticipantState participantState, int athleteId, int horseId)
        {
            this.state.ValidateThatEventHasNotStarted();

            var athlete = this.state.Athletes.FindDomain(athleteId);
            var horse = this.state.Horses.FindDomain(horseId);
            this.Validate<ParticipantException>(() =>
            {
                if (this.IsPartOfAnotherParticipant(athlete, participantState.Id))
                {
                    var message = string.Format(ALREADY_PARTICIPATING_TEMPLATE, athlete.Name);
                    throw new DomainException(message);
                }
                if (this.IsPartOfAnotherParticipant(horse, participantState.Id))
                {
                    var message = string.Format(ALREADY_PARTICIPATING_TEMPLATE, horse.Name);
                    throw new DomainException(message);
                }
            });

            var participant = this.state.Participants.FindDomain(participantState.Id);
            if (participant == null)
            {
                participant = new Participant(athlete, horse, participantState);
                this.state.Participants.AddOrUpdate(participant);
            }
            else
            {
                participant.Athlete = athlete;
                participant.Horse = horse;
                participant.RfId = participantState.RfId;
                participant.MaxAverageSpeedInKmPh = participantState.MaxAverageSpeedInKmPh;
                participant.Number = participantState.Number;
            }

            return participant;
        }

        public void Remove(int id)
        {
            this.state.ValidateThatEventHasNotStarted();

            var participant = this.state.Participants.FindDomain(id);
            this.state.Participants.Remove(participant);
        }
        public void AddParticipation(int competitionId, int participantId)
        {
            this.state.ValidateThatEventHasNotStarted();

            var participant = this.state.Participants.FindDomain(participantId);
            var competition = this.state.Event.Competitions.FindDomain(competitionId);
            participant.ParticipateIn(competition);
        }

        private bool IsPartOfAnotherParticipant(Athlete athlete, int participantId)
            => this.state.Participants.Any(x => x.Athlete.Equals(athlete) && x.Id != participantId);

        private bool IsPartOfAnotherParticipant(Horse horse, int participantId)
            => this.state.Participants.Any(x => x.Horse.Equals(horse) && x.Id != participantId);

        public void __REVERT_START_PARTICIPATIONS__()
        {
            foreach (var participant in this.state.Participants)
            {
                participant.Participation.__REMOVE_PERFORMANCES__();
            }
        }
    }
}

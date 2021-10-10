using EnduranceJudge.Domain.Core.Exceptions;
using EnduranceJudge.Domain.Core.Extensions;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Domain.State.Participants;
using System.Linq;
using static EnduranceJudge.Localization.DesktopStrings;

namespace EnduranceJudge.Domain.Aggregates.Configuration
{
    public class ParticipantsManager : ManagerObjectBase
    {
        private readonly IState state;

        internal ParticipantsManager(IState state)
        {
            this.state = state;
        }

        public void Save(IParticipantState state, int athleteId, int horseId)
            => this.Validate<ParticipantException>(() =>
        {
            var athlete = this.state.Athletes.FindDomain(athleteId);
            var horse = this.state.Horses.FindDomain(horseId);

            if (this.IsNew(state.Id))
            {
                if (this.IsParticipating(athlete))
                {
                    var message = string.Format(ALREADY_PARTICIPATING_TEMPLATE, athlete.Name);
                    throw new DomainException(message);
                }
                if (this.IsParticipating(horse))
                {
                    var message = string.Format(ALREADY_PARTICIPATING_TEMPLATE, horse.Name);
                    throw new DomainException(message);
                }
            }

            var participants = new Participant(athlete, horse, state)
            {
                Id = state.Id,
            };
            this.state.Participants.Save(participants);
        });

        public void Remove(int id)
        {
            var participant = this.state.Participants.FindDomain(id);
            this.state.Participants.Remove(participant);
        }

        private bool IsNew(int id)
            => this.state.Participants.All(x => x.Id != id);

        private bool IsParticipating(Athlete athlete)
            => this.state.Participants.Any(x => x.Athlete.Equals(athlete));

        private bool IsParticipating(Horse horse)
            => this.state.Participants.Any(x => x.Horse.Equals(horse));
    }
}

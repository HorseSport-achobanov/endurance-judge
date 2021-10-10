using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Core.Exceptions;
using EnduranceJudge.Domain.Core.Extensions;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Horses;
using static EnduranceJudge.Localization.DesktopStrings;

namespace EnduranceJudge.Domain.Aggregates.Configuration
{
    public class HorsesManager : ManagerObjectBase
    {
        private readonly IState state;
        internal HorsesManager(IState state)
        {
            this.state = state;
        }

        public void Save(IHorseState state)
        {
            var horse = new Horse(state)
            {
                Id = state.Id,
            };
            this.state.Horses.Save(horse);

            this.UpdateParticipants(horse);
        }

        public void Remove(int id) => this.Validate<HorseException>(() =>
        {
            var horse = this.state.Horses.FindDomain(id);
            foreach (var participant in this.state.Participants)
            {
                if (participant.Athlete.Equals(horse))
                {
                    throw new DomainException(CANNOT_REMOVE_USED_IN_PARTICIPANT);
                }
            }

            this.state.Horses.Remove(horse);
        });

        private void UpdateParticipants(Horse horse)
        {
            foreach (var participant in this.state.Participants)
            {
                if (participant.Horse.Equals(horse))
                {
                    participant.Horse.MapFrom(horse);
                }
            }
        }
    }
}

using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Core.Exceptions;
using EnduranceJudge.Domain.Core.Extensions;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Athletes;
using static EnduranceJudge.Localization.DesktopStrings;

namespace EnduranceJudge.Domain.Aggregates.Configuration
{
    public class AthletesManger : ManagerObjectBase
    {
        private readonly IState state;

        internal AthletesManger(IState state)
        {
            this.state = state;
        }

        public void Save(IAthleteState state, int countryId)
        {
            var country = this.state.Countries.FindDomain(countryId);
            var athlete = new Athlete(state)
            {
                Id = state.Id,
                Country = country
            };
            this.state.Athletes.Save(athlete);

            this.UpdateParticipants(athlete);
        }

        public void Remove(int id) => this.Validate<AthleteException>(() =>
        {
            var athlete = this.state.Athletes.FindDomain(id);
            foreach (var participant in this.state.Participants)
            {
                if (participant.Athlete.Equals(athlete))
                {
                    throw new DomainException(CANNOT_REMOVE_USED_IN_PARTICIPANT);
                }
            }

            this.state.Athletes.Remove(athlete);
        });

        private void UpdateParticipants(Athlete athlete)
        {
            foreach (var participant in this.state.Participants)
            {
                if (participant.Athlete.Equals(athlete))
                {
                    participant.Athlete.MapFrom(athlete);
                }
            }
        }
    }
}

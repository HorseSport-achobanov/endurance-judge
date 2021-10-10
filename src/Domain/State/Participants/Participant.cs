using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Domain.State.Participations;
using static EnduranceJudge.Localization.DesktopStrings;

namespace EnduranceJudge.Domain.State.Participants
{
    public class Participant : DomainObjectBase<ParticipantException>, IParticipantState
    {
        public const int DEFAULT_MAX_AVERAGE_SPEED = 16;
        public const string NAME_FORMAT = "{0} - {1}";

        private Participant() {}

        public Participant(Athlete athlete, Horse horse, IParticipantState state)
            : this(athlete, horse)
        {
            this.RfId = state.RfId;
            this.MaxAverageSpeedInKmPh = state.MaxAverageSpeedInKmPh;
            this.Number = state.Number;
        }
        public Participant(Athlete athlete, Horse horse) : base(true) => this.Validate(() =>
        {
            this.Horse = horse.IsRequired(HORSE);
            this.Athlete = athlete.IsRequired(ATHLETE);
        });

        public string RfId { get; private set; }
        public int Number { get; private set; }
        public int? MaxAverageSpeedInKmPh { get; private set; }
        public Horse Horse { get; private set; }
        public Athlete Athlete { get; private set; }
        public Participation Participation { get; private set; }

        public void ParticipateIn(Competition competition)
        {
            if (this.Participation == null)
            {
                this.Participation = new Participation(competition);
            }
            else
            {
                this.Participation.Add(competition);
            }
        }

        public string Name => string.Format(NAME_FORMAT, this.Athlete.Name, this.Horse.Name);
    }
}

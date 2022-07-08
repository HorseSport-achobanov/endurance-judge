using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.State.Participants;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Domain.State.Participations;

public class Participation : DomainBase<ParticipationException>
{
    internal Participation() {}
    internal Participation(Participant participant, Competition competition) : base(GENERATE_ID)
    {
        this.Participant = participant;
        this.NewParticipant = new NewParticipant
        {
            RfId = participant.RfId,
            Number = participant.Number,
            Horse = participant.Horse,
            Athlete = participant.Athlete,
            MaxAverageSpeedInKmPh = participant.MaxAverageSpeedInKmPh,
        };
        this.CompetitionConstraint = competition;
    }

    private List<int> competitionsIds = new();
    public Participant Participant { get; private set; }
    public NewParticipant NewParticipant { get; private set; }
    public Competition CompetitionConstraint { get; private set; }

    public double? Distance
        => this.CompetitionConstraint
            ?.Laps
            .Select(x => x.LengthInKm)
            .Sum();

    internal void Add(int competitionId)
    {
        this.competitionsIds.Add(competitionId);
    }
    internal void Remove(int competitionId)
    {
        this.competitionsIds.Remove(competitionId);
    }

    public IReadOnlyList<int> CompetitionsIds
    {
        get => this.competitionsIds.AsReadOnly();
        private set => this.competitionsIds = value.ToList();
    }

    public void __REMOVE_PERFORMANCES__()
    {
        this.Participant.__REMOVE_RECORDS__();
    }
}

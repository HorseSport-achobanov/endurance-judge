using EnduranceJudge.Domain.AggregateRoots.PoC.Events;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.LapRecords;
using EnduranceJudge.Domain.State.Participants;
using System.Linq;

namespace EnduranceJudge.Domain.AggregateRoots.PoC.Root;

public class PocRoot : INewAggregateRoot
{
    private readonly IState state;
    
    public PocRoot(IStateContext context)
    {
        this.state = context.State;

        PoCEvents.ParticipantChange += this.OnParticipantChange;
        PoCEvents.LapRecordChange += this.OnLapRecordChange;
    }

    public void OnParticipantChange(object sender, StateChangeArguments<NewParticipant> args)
    {
        var number = args.ChangedObject.Number;
        var participation = this.state.Participations.First(x => x.Participant.Number == number);

        var domain = new PocParticipant(args.ChangedObject, participation.CompetitionConstraint);

        if (domain.IsValid)
        {
            PoCEvents.RaiseParticipantChanged(args);
        }
    }

    public void OnLapRecordChange(object sender, StateChangeArguments<NewLapRecord> args)
    {
        var participant = this.state.Participants.First(x => x.NewLapRecords.Contains(args.ChangedObject));
        var participation = this.state.Participations.First(x => x.Participant.Number != participant.Number);

        var domain = new PocLap(args.ChangedObject, participation.CompetitionConstraint.Type);

        if (domain.IsValid)
        {
            PoCEvents.RaiseLapChanged(args);
        }
    }
}

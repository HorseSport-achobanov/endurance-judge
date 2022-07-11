using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.AggregateRoots.PoC.Events;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.LapRecords;
using EnduranceJudge.Domain.State.Participants;
using System;
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
        PoCEvents.Start += this.OnStart;
        PoCEvents.SetLapTime += this.OnSetLapTime;
    }
    private void OnSetLapTime(object sender, (string number, DateTime time) args)
    {
        var (number, time) = args;
        var participation = this.state.Participations.FirstOrDefault(x => x.Participant.Number == number);
        if (participation == null)
        {
            //TODO: Raise generic invalid event
            throw new NotImplementedException();
        }
        var participant = participation.NewParticipant;
        var competition = participation.CompetitionConstraint;

        var participantDomain = new PocParticipant(participant, competition);
        var isValid = participantDomain.SetLapTime(time);
        if (isValid)
        {
            PoCEvents.RaiseDomainChanged(participant);
        }
    }

    private void OnStart()
    {
        var participants = this.state.Participations
            .Select(x => (x.NewParticipant, x.CompetitionConstraint))
            .ToList();
        foreach (var (participant, competition) in participants)
        {
            var domain = new PocParticipant(participant, competition);
            domain.Start();
            
            if (!domain.IsValid)
            {
                return;
            }
        }

        PoCEvents.RaiseDomainChanged(participants.Select(x => x.NewParticipant));
    }

    public void OnParticipantChange(object sender, StateChangeArguments<NewParticipant> args)
    {
        var number = args.ChangedObject.Number;
        var participation = this.state.Participations.First(x => x.Participant.Number == number);

        var domain = new PocParticipant(args.ChangedObject, participation.CompetitionConstraint);

        if (domain.IsValid)
        {
            PoCEvents.RaiseDomainChanged(args.ChangedObject);
        }
    }

    public void OnLapRecordChange(object sender, StateChangeArguments<NewLapRecord> args)
    {
        var participation = this.state.Participations.First(x =>
            x.NewParticipant.LapRecords.Contains(args.ChangedObject));

        var domain = new PocLap(args.ChangedObject, participation.CompetitionConstraint.Type);

        if (domain.IsValid)
        {
            PoCEvents.RaiseDomainChanged(args.ChangedObject);
        }
    }
}

using EnduranceJudge.Domain.AggregateRoots.PoC.Events;
using EnduranceJudge.Domain.State.LapRecords;
using EnduranceJudge.Domain.State.Participants;

namespace EnduranceJudge.Domain.AggregateRoots.PoC.Root;

public static class PoCEvents
{
    public static event StateChangeHandler<NewParticipant> ParticipantChange;
    public static event StateChangeHandler<NewLapRecord> LapRecordChange;
    public static event DomainChangedHandler<NewParticipant> ParticipantChanged; 
    public static event DomainChangedHandler<NewLapRecord> LapChanged; 

    public static void RaiseParticipantChange(StateChangeArguments<NewParticipant> args)
    {
        ParticipantChange?.Invoke(null, args);
    }
    public static void RaiseLapRecordChange(StateChangeArguments<NewLapRecord> args)
    {
        LapRecordChange?.Invoke(null, args);
    } 
    public static void RaiseParticipantChanged(StateChangeArguments<NewParticipant> args)
    {
        ParticipantChanged?.Invoke(null, args);
    }
    public static void RaiseLapChanged(StateChangeArguments<NewLapRecord> args)
    {
        LapChanged?.Invoke(null, args);
    }
}

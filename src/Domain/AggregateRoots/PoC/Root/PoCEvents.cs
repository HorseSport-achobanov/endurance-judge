using EnduranceJudge.Domain.AggregateRoots.PoC.Events;
using EnduranceJudge.Domain.State.LapRecords;
using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Domain.State.Results;
using System;

namespace EnduranceJudge.Domain.AggregateRoots.PoC.Root;

public static class PoCEvents
{
    public static event StateChangeHandler<NewParticipant> ParticipantChange;
    public static event StateChangeHandler<NewLapRecord> LapRecordChange;
    public static event DomainChangedHandler<NewParticipant> ParticipantChanged; 
    public static event DomainChangedHandler<NewLapRecord> LapChanged;
    public static event StartEventHandler Start;
    public static event EventHandler<(string number, DateTime time)> SetLapTime;
    public static event EventHandler<(string number, ResultType result, string message)> Disqualify;

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
    public static void RaiseStart()
    {
        Start?.Invoke();
    }
    public static void RaiseSetLapTime(string number, DateTime time)
    {
        SetLapTime?.Invoke(null, (number, time));
    }
    public static void RaiseDisqualify(string number, ResultType type, string message)
    {
        Disqualify?.Invoke(null, (number, type, message));
    }
}

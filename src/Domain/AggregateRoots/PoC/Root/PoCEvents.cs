using EnduranceJudge.Domain.AggregateRoots.PoC.Events;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State.LapRecords;
using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Domain.State.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Domain.AggregateRoots.PoC.Root;

public static class PoCEvents
{
    public static event StateChangeHandler<NewParticipant> ParticipantChange;
    public static event StateChangeHandler<NewLapRecord> LapRecordChange;
    public static event DomainChangedHandler<NewParticipant> DomainChanged; 
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
    public static void RaiseDomainChanged(IDomain obj)
    {
        var args = new DomainChangedArguments(obj);
        DomainChanged?.Invoke(null, args);
    }
    public static void RaiseDomainChanged(IEnumerable<IDomain> obj)
    {
        var args = new DomainChangedArguments(obj.ToArray());
        DomainChanged?.Invoke(null, args);
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

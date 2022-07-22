﻿using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.State.LapRecords;
using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Domain.State.Results;
using System;
using System.Linq;

namespace EnduranceJudge.Domain.AggregateRoots.PoC;

// TODO: During app refactoring consider splitting the state by Aggregates
// I.e. ImportState, ConfigurationState, ManagerState, RankingState
// That would also require reloading the state when navigating through aggregate boundaries 
// But will also allow for true Aggregates, without reference to each other
// like here: LapRecord has to Reference the Lap (Configuration aggregate) to fetch settings
// instead of just containing those within itself.
internal class PocParticipant
{
    private readonly NewParticipant participant;
    private readonly Competition competition;
    
    public PocParticipant(NewParticipant participant, Competition competition)
    {
        this.competition = competition;
        this.participant = participant;

        var isInvalid = this.IsNewLapInvalid(participant) || this.ProcessLaps(participant);

        this.IsValid = !isInvalid;
    }
    
    public bool IsValid { get; }

    public bool Start()
    {
        if (this.participant.LapRecords.Any())
        {
            this.participant.RaiseInvalidChange("Competitions is already started.");
            return false;
        }
        this.CreateLap(this.competition.StartTime);
        return true;
    }
    
    public bool SetLapTime(DateTime time)
    {
        if (!this.participant.LapRecords.Any())
        {
            this.participant.RaiseInvalidChange($"Cannot set Lap time. Competition has not started yet.");
            return false;
        }
        
        var lap = this.GetCurrentLapDomain();
        if (lap.Record.ArrivalTime == null)
        {
            lap.Arrive(time);
        }
        else if (lap.Record.InspectionTime == null)
        {
            lap.Inspect(time);
        }
        else if (lap.Record.IsReInspectionRequired && lap.Record.ReInspectionTime == null)
        {
            lap.ReInspect(time);
        }
        else
        {
            lap.Complete();
            if (!this.IsComplete(this.participant))
            {
                this.CreateLap(lap.Record.NextStartTime!.Value);
                return this.SetLapTime(time);
            }
        }

        return true;
    }

    public bool Disqualify(ResultType type, string message)
    {
        var lap = this.GetCurrentLapDomain();
        lap.Complete(type, message);
        return true;
    }

    private PocLap GetCurrentLapDomain()
    {
        var record = this.participant.LapRecords.Last();
        return new PocLap(record, this.competition.Type);
    }

    private void CreateLap(DateTime startTime)
    {
        var config = this.competition.Laps
            .Skip(this.participant.LapRecords.Count)
            .First();
        var lengths = this.participant.LapRecords.Select(x => x.LengthInKm);
        var averageSpeeds = this.participant.LapRecords.Select(x => x.AverageSpeed!.Value);
        
        var newLap = new NewLapRecord(startTime, null, config, lengths, averageSpeeds);
        this.participant.LapRecords.Add(newLap);
    }
    
    private bool IsComplete(NewParticipant participant)
    {
        if (participant.LapRecords.Count == this.competition.Laps.Count
            && participant.LapRecords.All(x => x.Result is not null))
        {
            participant.RaiseInvalidChange(nameof(participant.LapRecords), "Participation is complete");
            return true;
        }
        return false;
    }

    private bool IsNewLapInvalid(NewParticipant participant)
    {
        if (participant.LapRecords.Count > 1)
        {
            var newLap = participant.LapRecords.Last();
            var previous = participant.LapRecords[^2];
            if (newLap.StartTime < previous.NextStartTime)
            {
                var message = $"Invalid date '{newLap.StartTime}'. Lap cannot start before '{previous.NextStartTime}'";
                newLap.RaiseInvalidChange(nameof(newLap.StartTime), message);
                return true;
            }
        }
        return false;
    }
    
    private bool ProcessLaps(NewParticipant participant)
    {
        foreach (var lap in participant.LapRecords)
        {
            var lapDomain = new PocLap(lap, this.competition.Type);
            if (!lapDomain.IsValid)
            {
                return true;
            }
        }
        return false;
    }
    
}
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.State.LapRecords;
using EnduranceJudge.Domain.State.Results;
using System;
using System.Linq;
using static EnduranceJudge.Localization.Strings;

namespace EnduranceJudge.Domain.AggregateRoots.PoC;

//TODO: Probably inherit State model
// That would mean that DomainModels are used as the actual objects
// in the AppState, rather than state objects
internal class PocLap
{
    // TODO: probably move that to setting on Event level
    public const int COMPULSORY_INSPECTION_TIME_OFFSET = -15;
    private readonly NewLapRecord lap;
    private readonly CompetitionType competitionType;

    public PocLap(NewLapRecord lap, CompetitionType competitionType)
    {
        this.lap = lap;
        this.competitionType = competitionType;

        var areTimesValid = this.IsValidTime(this.lap.ArrivalTime, this.lap.StartTime, ARRIVAL_TERM)
            || this.IsValidTime(this.lap.InspectionTime, this.lap.ArrivalTime, INSPECTION_TERM)
            || this.IsValidTime(this.lap.ReInspectionTime, this.lap.InspectionTime, RE_INSPECTION_TERM);
        if (!areTimesValid)
        {
            this.IsValid = false;
            return;
        } 

        this.lap.TotalLength = this.CalculateTotalLength();
        this.GenerateLapPerformanceData();
    }

    public NewLapRecord Record => this.lap;
    public bool IsValid { get; private set; }

    public void Arrive(DateTime time)
    {
        if (this.IsValidTime(time, this.lap.StartTime, ARRIVAL_TERM))
        {
            this.lap.ArrivalTime = time;
        }
    }
    public void Inspect(DateTime time)
    {
        if (this.IsValidTime(time, this.lap.ArrivalTime, INSPECTION_TERM))
        {
            this.lap.InspectionTime = time;
        }
        if (!this.lap.IsReInspectionRequired)
        {
            this.GenerateLapPerformanceData();
        }
    }
    public void ReInspect(DateTime time)
    {
        if (this.IsValidTime(time, this.lap.InspectionTime, RE_INSPECTION_TERM))
        {
            this.lap.ReInspectionTime = time;
        }
        this.GenerateLapPerformanceData();
    }
    public void Complete(ResultType? type = null, string message = null)
    {
        this.lap.Result = new Result(type ?? ResultType.Successful, message);
    }

    private bool IsValidTime(DateTime? time, DateTime? previous, string term)
    {
        if (time.HasValue && !previous.HasValue)
        {
            throw new Exception($"Unable to perform '{term}' because previous time is null.");
        }
        if (time <= previous)
        {
            var message = string.Format(DATE_TIME_HAS_TO_BE_LATER_MESSAGE, term, time, previous.Value);
            this.lap.RaiseInvalidChange(message);
            return false;
        }
        return true;
    }

    private void GenerateLapPerformanceData()
    {
        var vetTime = GetVetTime(this.lap);
        if (vetTime == null)
        {
            return;
        }
        
        this.lap.NextStartTime = vetTime.Value.AddMinutes(this.lap.RestTimeInMins);
        if (this.lap.IsRequiredInspectionRequired || this.lap.IsCompulsoryInspectionRequired)
        {
            this.lap.RequiredInspectionTime = this.CalculateRequiredInspectionTime();
        }
        this.lap.RecoverySpan = this.CalculateRecoverySpan(vetTime.Value);
        this.lap.Time = this.CalculateCurrentTime();
        this.lap.AverageSpeed = this.CalculateAverageSpeed();
        this.lap.AverageSpeedTotal = this.CalculateTotalAverageSpeed();
    }

    private DateTime? CalculateRequiredInspectionTime()
        => this.lap.NextStartTime?.AddMinutes(COMPULSORY_INSPECTION_TIME_OFFSET);
    
    private TimeSpan? CalculateRecoverySpan(DateTime vetTime)
        => vetTime - this.lap.ArrivalTime;

    public TimeSpan CalculateCurrentTime()
        => CalculateTime(this.lap, this.competitionType)!.Value;

    private double? CalculateAverageSpeed()
        => this.lap.LengthInKm / this.lap.Time?.TotalHours;

    private double? CalculateTotalAverageSpeed()
    {
        if (this.lap.PreviousAverageSpeeds.Any())
        {
            var sum = this.lap.PreviousAverageSpeeds.Sum() + this.lap.AverageSpeed;
            var count = this.lap.PreviousAverageSpeeds.Count + 1;
            return sum / count;
        }
        return this.lap.AverageSpeed;
    }

    private double CalculateTotalLength()
        => this.lap.PreviousLengths.Any()
            ? this.lap.PreviousLengths.Sum() + this.lap.LengthInKm
            : this.lap.LengthInKm;

    // TODO: probably private
    public static TimeSpan? CalculateTime(NewLapRecord lap, CompetitionType competitionType) 
        => competitionType == CompetitionType.National || lap.IsFinal
            ? lap.ArrivalTime!.Value - lap.StartTime
            : GetVetTime(lap) - lap.StartTime;

    public static DateTime? GetVetTime(NewLapRecord lap)
        => lap.ReInspectionTime ?? lap.InspectionTime;
}

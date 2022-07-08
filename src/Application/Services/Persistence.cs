using EnduranceJudge.Application.Core.Services;
using EnduranceJudge.Application.Models;
using EnduranceJudge.Application.State;
using EnduranceJudge.Core.ConventionalServices;
using EnduranceJudge.Core.Services;
using EnduranceJudge.Domain.AggregateRoots.Manager.WitnessEvents;
using EnduranceJudge.Domain.AggregateRoots.PoC.Root;
using EnduranceJudge.Domain.State;
using System;
using System.Collections.Generic;

namespace EnduranceJudge.Application.Services;

public class Persistence : IPersistence
{
    private const string STORAGE_FILE_NAME = "judge.data";
    private string stateDirectoryPath;

    private readonly IStateSetter stateSetter;
    private readonly IState state;
    private readonly IFileService file;
    private readonly IJsonSerializationService serialization;
    
    public Persistence(
        IStateContext context,
        IStateSetter stateSetter,
        IFileService file,
        IJsonSerializationService serialization)
    {
        this.stateSetter = stateSetter;
        this.state = context.State;
        this.file = file;
        this.serialization = serialization;
        Witness.StateChanged += (_, _) => this.SaveState();

        PoCEvents.DomainChanged += (_, _) => this.SaveState();
    }

    public string LogError(string message, string stackTrace)
    {
        var timestamp = DateTime.Now.ToString("yyyy-mm-ddTHH-mm-ss");
        var log = new Dictionary<string, object>
        {
            { "error-message", message },
            { "error-stack-trance", stackTrace },
            { "state", this.state },
        };
        var serialized = this.serialization.Serialize(log);
        var filename = $"{timestamp}_error.json";
        var path = $"{this.stateDirectoryPath}/{filename}";
        this.file.Create(path, serialized);
        return path;
    }

    public PersistenceResult Configure(string directoryPath)
    {
        this.stateDirectoryPath = directoryPath;
        var database = BuildStorageFilePath(directoryPath);
        if (this.file.Exists(database))
        {
            this.SetState();
            return PersistenceResult.Existing;
        }
        
        this.SaveState();
        return PersistenceResult.New;
    }
    
    public void SaveState()
    {
        var serialized = this.serialization.Serialize(this.state);
        var databasePath = BuildStorageFilePath(this.stateDirectoryPath);
        this.file.Create(databasePath, serialized);
    }
    
    private void SetState()
    {
        var dataPath = BuildStorageFilePath(this.stateDirectoryPath);
        var contents = this.file.Read(dataPath);
        var state = this.serialization.Deserialize<StateModel>(contents);
        // this.FixDatesForToday(state);
        this.stateSetter.Set(state);
    }

    private static string BuildStorageFilePath(string directory) => $"{directory}\\{STORAGE_FILE_NAME}";
    
     
    // TODO: add opt-in configuration
    private void FixDatesForToday(StateModel state)
    {
        if (state == null)
        {
            return;
        }
        if (state.Event != null)
        {
            foreach (var competition in state.Event.Competitions)
            {
                competition.StartTime = FixDateForToday(competition.StartTime);
            }
        }
        foreach (var participant in state.Participants)
        {
            foreach (var performance in participant.LapRecords)
            {
                performance.StartTime = FixDateForToday(performance.StartTime);
                if (performance.ArrivalTime.HasValue)
                {
                    performance.ArrivalTime = FixDateForToday(performance.ArrivalTime.Value);
                }
                if (performance.InspectionTime.HasValue)
                {
                    performance.InspectionTime = FixDateForToday(performance.InspectionTime.Value);
                }
                if (performance.ReInspectionTime.HasValue)
                {
                    performance.ReInspectionTime = FixDateForToday(performance.ReInspectionTime.Value);
                }
            }
        }
    }

    private DateTime FixDateForToday(DateTime date)
    {
        var today = DateTime.Today;
        today = today.AddHours(date.Hour);
        today = today.AddMinutes(date.Minute);
        today = today.AddSeconds(date.Second);
        today = today.AddMilliseconds(date.Millisecond);
        return today;
    }
}


public interface IPersistence : ISingletonService
{
    void SaveState();
    string LogError(string message, string stackTrace);
    PersistenceResult Configure(string directoryPath);
}

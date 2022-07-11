using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Domain.State.LapRecords;
using System.Collections.ObjectModel;

namespace EnduranceJudge.Domain.State.Participants;

public class NewParticipant : NewDomainBase, IParticipantState
{
    private ObservableCollection<NewLapRecord> lapRecords = new();
    private string rfid;
    private string number;
    private int? maxAverageSpeedInKmPh;
    private Horse horse;
    private Athlete athlete;

    private NewParticipant()
    {
    }
    public NewParticipant(string rfId, string number, Horse horse, Athlete athlete, int? maxAverageSpeedInKmPh)
        : base(GENERATE_ID)
    {
        this.RfId = rfId;
        this.Number = number;
        this.Horse = horse;
        this.Athlete = athlete;
        this.MaxAverageSpeedInKmPh = maxAverageSpeedInKmPh;
    }
    
    public Horse Horse 
    { 
        get => this.horse;
        internal set => this.SetValue(ref this.horse, value);
    }
    public Athlete Athlete
    {
        get => this.athlete;
        internal set => this.SetValue(ref this.athlete, value);
    }
    public string RfId
    { 
        get => this.rfid;
        set => this.SetValue(ref this.rfid, value);
    }
    public string Number
    { 
        get => this.number;
        set => this.SetValue(ref this.number, value);
    }
    public int? MaxAverageSpeedInKmPh
    { 
        get => this.maxAverageSpeedInKmPh;
        set => this.SetValue(ref this.maxAverageSpeedInKmPh, value);
    }
    public ObservableCollection<NewLapRecord> LapRecords
    {
        get => this.lapRecords;
        private set => this.SetValue(ref this.lapRecords, new ObservableCollection<NewLapRecord>(value));
    }
    public void Add(NewLapRecord record)
    {
        this.LapRecords.Add(record);
        this.RaisePropertyChanged(nameof(this.LapRecords));
    }
}

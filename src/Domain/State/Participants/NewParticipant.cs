using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State.LapRecords;
using System.Collections.ObjectModel;

namespace EnduranceJudge.Domain.State.Participants;

public class NewParticipant : NewDomainBase, IParticipantState
{
    private ObservableCollection<NewLapRecord> lapRecords = new();
    private string rfid;
    private string number;
    private int? maxAverageSpeedInKmPh;

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
    public ObservableCollection<NewLapRecord> LapRecords => this.LapRecords;
}

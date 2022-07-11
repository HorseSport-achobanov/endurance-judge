using EnduranceJudge.Domain.State.LapRecords;
using EnduranceJudge.Gateways.Desktop.Converters;
using static EnduranceJudge.Localization.Strings;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.PoC.Converters;

public class LapHeaderConverter : XamlConverterBase<NewLapRecord, string>
{
    protected override string Convert(NewLapRecord source)
    {
        var lap = source.IsFinal
            ? $"{FINAL}"
            : $"{GATE.ToUpper()}{source.PreviousLengths.Count + 1}";
        var header = $"{lap}/{source.TotalLength} {KM}";
        return header;
    }
}

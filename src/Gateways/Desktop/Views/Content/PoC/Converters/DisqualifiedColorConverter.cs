using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Gateways.Desktop.Converters;
using System.Linq;
using System.Windows.Media;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.PoC.Converters;

public class ParticipantToBrushConverter : XamlConverterBase<NewParticipant, SolidColorBrush>
{
    protected override SolidColorBrush Convert(NewParticipant source)
    {
        var color = source.LapRecords.Any(x => x.Result?.IsNotQualified ?? false)
            ? Colors.Red
            : Colors.Black;
        return new SolidColorBrush(color);   
    }
}

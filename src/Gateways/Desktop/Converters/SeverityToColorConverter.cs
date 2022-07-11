using EnduranceJudge.Gateways.Desktop.Core.Objects;
using System;
using System.Windows.Media;

namespace EnduranceJudge.Gateways.Desktop.Converters;

public class SeverityToColorConverter : XamlConverterBase<MessageSeverity, SolidColorBrush>
{
    protected override SolidColorBrush Convert(MessageSeverity source)
    {
        var color = source switch
        {
            MessageSeverity.Warning => Colors.Goldenrod,
            MessageSeverity.Error => Colors.DarkRed,
            MessageSeverity.Success => Colors.Green,
            _ => throw new ArgumentOutOfRangeException($"Invalid Message severity: '{source}'"),
        };
        return new SolidColorBrush(color);
    }
}

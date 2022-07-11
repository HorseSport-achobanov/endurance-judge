using EnduranceJudge.Gateways.Desktop.Converters;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Core.Components.Converters;

public class VisibilityToBooleanConverter : XamlConverterBase<Visibility, bool>
{
    protected override bool Convert(Visibility source)
        => source == Visibility.Visible;
}

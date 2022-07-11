using EnduranceJudge.Gateways.Desktop.Converters;
using EnduranceJudge.Gateways.Desktop.Core;
using static EnduranceJudge.Localization.Strings;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.PoC.Converters;

public class DoubleToSpeedConverter : XamlConverterBase<double, string>
{
    protected override string Convert(double source)
    {
        var value = ValueSerializer.FormatDouble(source);
        return $"{value} {KM_PER_HOUR}";
    }
}

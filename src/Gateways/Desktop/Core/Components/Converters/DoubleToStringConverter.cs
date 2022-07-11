using EnduranceJudge.Gateways.Desktop.Converters;

namespace EnduranceJudge.Gateways.Desktop.Core.Components.Converters;

public class DoubleToStringConverter : XamlConverterBase<double, string>
{
    protected override string Convert(double source)
        => ValueSerializer.FormatDouble(source);
}
